using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Reflection;
using NLog;
using ObjectWeb.Asm;
using ObjectWeb.Asm.Tree;
using static ObjectWeb.Asm.Opcodes;

namespace Java_Corruptor;

public static class AsmUtilities
{
    public static readonly BidirectionalDictionary<int, string> Opcodes = new(159);
    public static readonly BidirectionalDictionary<int, string> Tags = new(9);
    public static readonly Dictionary<int, int> Types = new(159);
    public static readonly ConcurrentDictionary<string, (ClassNode node, byte[] bytes, string path)> Classes = [];
    public static readonly ConcurrentDictionary<string, byte[]> NonClasses = [];
    
    private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

    static AsmUtilities()
    {
        foreach (FieldInfo field in typeof(Opcodes).GetFields(BindingFlags.Public | BindingFlags.Static))
        {
            if (field.Name.StartsWith("H_"))
                Tags.Add((int)field.GetValue(null), field.Name.ToUpper());
        }

        #region Add string versions of each opcode to the Opcodes dictionary.

        Opcodes.Add(-1, "LINE");
        Opcodes.Add(Aaload, "AALOAD");
        Opcodes.Add(Aastore, "AASTORE");
        Opcodes.Add(Aconst_Null, "ACONST_NULL");
        Opcodes.Add(Aload, "ALOAD");
        Opcodes.Add(Anewarray, "ANEWARRAY");
        Opcodes.Add(Areturn, "ARETURN");
        Opcodes.Add(Arraylength, "ARRAYLENGTH");
        Opcodes.Add(Astore, "ASTORE");
        Opcodes.Add(Athrow, "ATHROW");
        Opcodes.Add(Baload, "BALOAD");
        Opcodes.Add(Bastore, "BASTORE");
        Opcodes.Add(Bipush, "BIPUSH");
        Opcodes.Add(Caload, "CALOAD");
        Opcodes.Add(Castore, "CASTORE");
        Opcodes.Add(Checkcast, "CHECKCAST");
        Opcodes.Add(D2F, "D2F");
        Opcodes.Add(D2I, "D2I");
        Opcodes.Add(D2L, "D2L");
        Opcodes.Add(Dadd, "DADD");
        Opcodes.Add(Daload, "DALOAD");
        Opcodes.Add(Dastore, "DASTORE");
        Opcodes.Add(Dcmpg, "DCMPG");
        Opcodes.Add(Dcmpl, "DCMPL");
        Opcodes.Add(Dconst_0, "DCONST_0");
        Opcodes.Add(Dconst_1, "DCONST_1");
        Opcodes.Add(Ddiv, "DDIV");
        Opcodes.Add(Dload, "DLOAD");
        Opcodes.Add(Dmul, "DMUL");
        Opcodes.Add(Dneg, "DNEG");
        Opcodes.Add(Drem, "DREM");
        Opcodes.Add(Dreturn, "DRETURN");
        Opcodes.Add(Dstore, "DSTORE");
        Opcodes.Add(Dsub, "DSUB");
        Opcodes.Add(Dup, "DUP");
        Opcodes.Add(Dup2, "DUP2");
        Opcodes.Add(Dup2_X1, "DUP2_X1");
        Opcodes.Add(Dup2_X2, "DUP2_X2");
        Opcodes.Add(Dup_X1, "DUP_X1");
        Opcodes.Add(Dup_X2, "DUP_X2");
        Opcodes.Add(F2D, "F2D");
        Opcodes.Add(F2I, "F2I");
        Opcodes.Add(F2L, "F2L");
        Opcodes.Add(Fadd, "FADD");
        Opcodes.Add(Faload, "FALOAD");
        Opcodes.Add(Fastore, "FASTORE");
        Opcodes.Add(Fcmpg, "FCMPG");
        Opcodes.Add(Fcmpl, "FCMPL");
        Opcodes.Add(Fconst_0, "FCONST_0");
        Opcodes.Add(Fconst_1, "FCONST_1");
        Opcodes.Add(Fconst_2, "FCONST_2");
        Opcodes.Add(Fdiv, "FDIV");
        Opcodes.Add(Fload, "FLOAD");
        Opcodes.Add(Fmul, "FMUL");
        Opcodes.Add(Fneg, "FNEG");
        Opcodes.Add(Frem, "FREM");
        Opcodes.Add(Freturn, "FRETURN");
        Opcodes.Add(Fstore, "FSTORE");
        Opcodes.Add(Fsub, "FSUB");
        Opcodes.Add(Getfield, "GETFIELD");
        Opcodes.Add(Getstatic, "GETSTATIC");
        Opcodes.Add(Goto, "GOTO");
        Opcodes.Add(I2B, "I2B");
        Opcodes.Add(I2C, "I2C");
        Opcodes.Add(I2D, "I2D");
        Opcodes.Add(I2F, "I2F");
        Opcodes.Add(I2L, "I2L");
        Opcodes.Add(I2S, "I2S");
        Opcodes.Add(Iadd, "IADD");
        Opcodes.Add(Iaload, "IALOAD");
        Opcodes.Add(Iand, "IAND");
        Opcodes.Add(Iastore, "IASTORE");
        Opcodes.Add(Iconst_0, "ICONST_0");
        Opcodes.Add(Iconst_1, "ICONST_1");
        Opcodes.Add(Iconst_2, "ICONST_2");
        Opcodes.Add(Iconst_3, "ICONST_3");
        Opcodes.Add(Iconst_4, "ICONST_4");
        Opcodes.Add(Iconst_5, "ICONST_5");
        Opcodes.Add(Iconst_M1, "ICONST_M1");
        Opcodes.Add(Idiv, "IDIV");
        Opcodes.Add(If_Acmpeq, "IF_ACMPEQ");
        Opcodes.Add(If_Acmpne, "IF_ACMPNE");
        Opcodes.Add(If_Icmpeq, "IF_ICMPEQ");
        Opcodes.Add(If_Icmpge, "IF_ICMPGE");
        Opcodes.Add(If_Icmpgt, "IF_ICMPGT");
        Opcodes.Add(If_Icmple, "IF_ICMPLE");
        Opcodes.Add(If_Icmplt, "IF_ICMPLT");
        Opcodes.Add(If_Icmpne, "IF_ICMPNE");
        Opcodes.Add(Ifeq, "IFEQ");
        Opcodes.Add(Ifge, "IFGE");
        Opcodes.Add(Ifgt, "IFGT");
        Opcodes.Add(Ifle, "IFLE");
        Opcodes.Add(Iflt, "IFLT");
        Opcodes.Add(Ifne, "IFNE");
        Opcodes.Add(Ifnonnull, "IFNONNULL");
        Opcodes.Add(Ifnull, "IFNULL");
        Opcodes.Add(Iinc, "IINC");
        Opcodes.Add(Iload, "ILOAD");
        Opcodes.Add(Imul, "IMUL");
        Opcodes.Add(Ineg, "INEG");
        Opcodes.Add(Instanceof, "INSTANCEOF");
        Opcodes.Add(Invokedynamic, "INVOKEDYNAMIC");
        Opcodes.Add(Invokeinterface, "INVOKEINTERFACE");
        Opcodes.Add(Invokespecial, "INVOKESPECIAL");
        Opcodes.Add(Invokestatic, "INVOKESTATIC");
        Opcodes.Add(Invokevirtual, "INVOKEVIRTUAL");
        Opcodes.Add(Ior, "IOR");
        Opcodes.Add(Irem, "IREM");
        Opcodes.Add(Ireturn, "IRETURN");
        Opcodes.Add(Ishl, "ISHL");
        Opcodes.Add(Ishr, "ISHR");
        Opcodes.Add(Istore, "ISTORE");
        Opcodes.Add(Isub, "ISUB");
        Opcodes.Add(Iushr, "IUSHR");
        Opcodes.Add(Ixor, "IXOR");
        Opcodes.Add(Jsr, "JSR");
        Opcodes.Add(L2D, "L2D");
        Opcodes.Add(L2F, "L2F");
        Opcodes.Add(L2I, "L2I");
        Opcodes.Add(Ladd, "LADD");
        Opcodes.Add(Laload, "LALOAD");
        Opcodes.Add(Land, "LAND");
        Opcodes.Add(Lastore, "LASTORE");
        Opcodes.Add(Lcmp, "LCMP");
        Opcodes.Add(Lconst_0, "LCONST_0");
        Opcodes.Add(Lconst_1, "LCONST_1");
        Opcodes.Add(Ldc, "LDC");
        Opcodes.Add(Ldiv, "LDIV");
        Opcodes.Add(Lload, "LLOAD");
        Opcodes.Add(Lmul, "LMUL");
        Opcodes.Add(Lneg, "LNEG");
        Opcodes.Add(Lookupswitch, "LOOKUPSWITCH");
        Opcodes.Add(Lor, "LOR");
        Opcodes.Add(Lrem, "LREM");
        Opcodes.Add(Lreturn, "LRETURN");
        Opcodes.Add(Lshl, "LSHL");
        Opcodes.Add(Lshr, "LSHR");
        Opcodes.Add(Lstore, "LSTORE");
        Opcodes.Add(Lsub, "LSUB");
        Opcodes.Add(Lushr, "LUSHR");
        Opcodes.Add(Lxor, "LXOR");
        Opcodes.Add(Monitorenter, "MONITORENTER");
        Opcodes.Add(Monitorexit, "MONITOREXIT");
        Opcodes.Add(Multianewarray, "MULTIANEWARRAY");
        Opcodes.Add(New, "NEW");
        Opcodes.Add(Newarray, "NEWARRAY");
        Opcodes.Add(Nop, "NOP");
        Opcodes.Add(Pop, "POP");
        Opcodes.Add(Pop2, "POP2");
        Opcodes.Add(Putfield, "PUTFIELD");
        Opcodes.Add(Putstatic, "PUTSTATIC");
        Opcodes.Add(Ret, "RET");
        Opcodes.Add(Return, "RETURN");
        Opcodes.Add(Saload, "SALOAD");
        Opcodes.Add(Sastore, "SASTORE");
        Opcodes.Add(Sipush, "SIPUSH");
        Opcodes.Add(Swap, "SWAP");
        Opcodes.Add(Tableswitch, "TABLESWITCH");

        #endregion

        #region Add the type of instruction each opcode is of to the Types dictionary.

        Types.Add(-1, AbstractInsnNode.Line_Insn);
        Types.Add(Nop, AbstractInsnNode.Insn);
        Types.Add(Aconst_Null, AbstractInsnNode.Insn);
        Types.Add(Iconst_M1, AbstractInsnNode.Insn);
        Types.Add(Iconst_0, AbstractInsnNode.Insn);
        Types.Add(Iconst_1, AbstractInsnNode.Insn);
        Types.Add(Iconst_2, AbstractInsnNode.Insn);
        Types.Add(Iconst_3, AbstractInsnNode.Insn);
        Types.Add(Iconst_4, AbstractInsnNode.Insn);
        Types.Add(Iconst_5, AbstractInsnNode.Insn);
        Types.Add(Lconst_0, AbstractInsnNode.Insn);
        Types.Add(Lconst_1, AbstractInsnNode.Insn);
        Types.Add(Fconst_0, AbstractInsnNode.Insn);
        Types.Add(Fconst_1, AbstractInsnNode.Insn);
        Types.Add(Fconst_2, AbstractInsnNode.Insn);
        Types.Add(Dconst_0, AbstractInsnNode.Insn);
        Types.Add(Dconst_1, AbstractInsnNode.Insn);
        Types.Add(Iaload, AbstractInsnNode.Insn);
        Types.Add(Laload, AbstractInsnNode.Insn);
        Types.Add(Faload, AbstractInsnNode.Insn);
        Types.Add(Daload, AbstractInsnNode.Insn);
        Types.Add(Aaload, AbstractInsnNode.Insn);
        Types.Add(Baload, AbstractInsnNode.Insn);
        Types.Add(Caload, AbstractInsnNode.Insn);
        Types.Add(Saload, AbstractInsnNode.Insn);
        Types.Add(Iastore, AbstractInsnNode.Insn);
        Types.Add(Lastore, AbstractInsnNode.Insn);
        Types.Add(Fastore, AbstractInsnNode.Insn);
        Types.Add(Dastore, AbstractInsnNode.Insn);
        Types.Add(Aastore, AbstractInsnNode.Insn);
        Types.Add(Bastore, AbstractInsnNode.Insn);
        Types.Add(Castore, AbstractInsnNode.Insn);
        Types.Add(Sastore, AbstractInsnNode.Insn);
        Types.Add(Pop, AbstractInsnNode.Insn);
        Types.Add(Pop2, AbstractInsnNode.Insn);
        Types.Add(Dup, AbstractInsnNode.Insn);
        Types.Add(Dup_X1, AbstractInsnNode.Insn);
        Types.Add(Dup_X2, AbstractInsnNode.Insn);
        Types.Add(Dup2, AbstractInsnNode.Insn);
        Types.Add(Dup2_X1, AbstractInsnNode.Insn);
        Types.Add(Dup2_X2, AbstractInsnNode.Insn);
        Types.Add(Swap, AbstractInsnNode.Insn);
        Types.Add(Iadd, AbstractInsnNode.Insn);
        Types.Add(Ladd, AbstractInsnNode.Insn);
        Types.Add(Fadd, AbstractInsnNode.Insn);
        Types.Add(Dadd, AbstractInsnNode.Insn);
        Types.Add(Isub, AbstractInsnNode.Insn);
        Types.Add(Lsub, AbstractInsnNode.Insn);
        Types.Add(Fsub, AbstractInsnNode.Insn);
        Types.Add(Dsub, AbstractInsnNode.Insn);
        Types.Add(Imul, AbstractInsnNode.Insn);
        Types.Add(Lmul, AbstractInsnNode.Insn);
        Types.Add(Fmul, AbstractInsnNode.Insn);
        Types.Add(Dmul, AbstractInsnNode.Insn);
        Types.Add(Idiv, AbstractInsnNode.Insn);
        Types.Add(Ldiv, AbstractInsnNode.Insn);
        Types.Add(Fdiv, AbstractInsnNode.Insn);
        Types.Add(Ddiv, AbstractInsnNode.Insn);
        Types.Add(Irem, AbstractInsnNode.Insn);
        Types.Add(Lrem, AbstractInsnNode.Insn);
        Types.Add(Frem, AbstractInsnNode.Insn);
        Types.Add(Drem, AbstractInsnNode.Insn);
        Types.Add(Ineg, AbstractInsnNode.Insn);
        Types.Add(Lneg, AbstractInsnNode.Insn);
        Types.Add(Fneg, AbstractInsnNode.Insn);
        Types.Add(Dneg, AbstractInsnNode.Insn);
        Types.Add(Ishl, AbstractInsnNode.Insn);
        Types.Add(Lshl, AbstractInsnNode.Insn);
        Types.Add(Ishr, AbstractInsnNode.Insn);
        Types.Add(Lshr, AbstractInsnNode.Insn);
        Types.Add(Iushr, AbstractInsnNode.Insn);
        Types.Add(Lushr, AbstractInsnNode.Insn);
        Types.Add(Iand, AbstractInsnNode.Insn);
        Types.Add(Land, AbstractInsnNode.Insn);
        Types.Add(Ior, AbstractInsnNode.Insn);
        Types.Add(Lor, AbstractInsnNode.Insn);
        Types.Add(Ixor, AbstractInsnNode.Insn);
        Types.Add(Lxor, AbstractInsnNode.Insn);
        Types.Add(I2L, AbstractInsnNode.Insn);
        Types.Add(I2F, AbstractInsnNode.Insn);
        Types.Add(I2D, AbstractInsnNode.Insn);
        Types.Add(L2I, AbstractInsnNode.Insn);
        Types.Add(L2F, AbstractInsnNode.Insn);
        Types.Add(L2D, AbstractInsnNode.Insn);
        Types.Add(F2I, AbstractInsnNode.Insn);
        Types.Add(F2L, AbstractInsnNode.Insn);
        Types.Add(F2D, AbstractInsnNode.Insn);
        Types.Add(D2I, AbstractInsnNode.Insn);
        Types.Add(D2L, AbstractInsnNode.Insn);
        Types.Add(D2F, AbstractInsnNode.Insn);
        Types.Add(I2B, AbstractInsnNode.Insn);
        Types.Add(I2C, AbstractInsnNode.Insn);
        Types.Add(I2S, AbstractInsnNode.Insn);
        Types.Add(Lcmp, AbstractInsnNode.Insn);
        Types.Add(Fcmpl, AbstractInsnNode.Insn);
        Types.Add(Fcmpg, AbstractInsnNode.Insn);
        Types.Add(Dcmpl, AbstractInsnNode.Insn);
        Types.Add(Dcmpg, AbstractInsnNode.Insn);
        Types.Add(Ireturn, AbstractInsnNode.Insn);
        Types.Add(Lreturn, AbstractInsnNode.Insn);
        Types.Add(Freturn, AbstractInsnNode.Insn);
        Types.Add(Dreturn, AbstractInsnNode.Insn);
        Types.Add(Areturn, AbstractInsnNode.Insn);
        Types.Add(Return, AbstractInsnNode.Insn);
        Types.Add(Arraylength, AbstractInsnNode.Insn);
        Types.Add(Athrow, AbstractInsnNode.Insn);
        Types.Add(Monitorenter, AbstractInsnNode.Insn);
        Types.Add(Monitorexit, AbstractInsnNode.Insn);
        Types.Add(Bipush, AbstractInsnNode.Int_Insn);
        Types.Add(Sipush, AbstractInsnNode.Int_Insn);
        Types.Add(Newarray, AbstractInsnNode.Int_Insn);
        Types.Add(Iload, AbstractInsnNode.Var_Insn);
        Types.Add(Lload, AbstractInsnNode.Var_Insn);
        Types.Add(Fload, AbstractInsnNode.Var_Insn);
        Types.Add(Dload, AbstractInsnNode.Var_Insn);
        Types.Add(Aload, AbstractInsnNode.Var_Insn);
        Types.Add(Istore, AbstractInsnNode.Var_Insn);
        Types.Add(Lstore, AbstractInsnNode.Var_Insn);
        Types.Add(Fstore, AbstractInsnNode.Var_Insn);
        Types.Add(Dstore, AbstractInsnNode.Var_Insn);
        Types.Add(Astore, AbstractInsnNode.Var_Insn);
        Types.Add(Ret, AbstractInsnNode.Var_Insn);
        Types.Add(New, AbstractInsnNode.Type_Insn);
        Types.Add(Anewarray, AbstractInsnNode.Type_Insn);
        Types.Add(Checkcast, AbstractInsnNode.Type_Insn);
        Types.Add(Instanceof, AbstractInsnNode.Type_Insn);
        Types.Add(Getstatic, AbstractInsnNode.Field_Insn);
        Types.Add(Getfield, AbstractInsnNode.Field_Insn);
        Types.Add(Putstatic, AbstractInsnNode.Field_Insn);
        Types.Add(Putfield, AbstractInsnNode.Field_Insn);
        Types.Add(Invokevirtual, AbstractInsnNode.Method_Insn);
        Types.Add(Invokespecial, AbstractInsnNode.Method_Insn);
        Types.Add(Invokestatic, AbstractInsnNode.Method_Insn);
        Types.Add(Invokeinterface, AbstractInsnNode.Method_Insn);
        Types.Add(Invokedynamic, AbstractInsnNode.Invoke_Dynamic_Insn);
        Types.Add(Ifeq, AbstractInsnNode.Jump_Insn);
        Types.Add(Ifne, AbstractInsnNode.Jump_Insn);
        Types.Add(Iflt, AbstractInsnNode.Jump_Insn);
        Types.Add(Ifge, AbstractInsnNode.Jump_Insn);
        Types.Add(Ifgt, AbstractInsnNode.Jump_Insn);
        Types.Add(Ifle, AbstractInsnNode.Jump_Insn);
        Types.Add(If_Icmpeq, AbstractInsnNode.Jump_Insn);
        Types.Add(If_Icmpne, AbstractInsnNode.Jump_Insn);
        Types.Add(If_Icmplt, AbstractInsnNode.Jump_Insn);
        Types.Add(If_Icmpge, AbstractInsnNode.Jump_Insn);
        Types.Add(If_Icmpgt, AbstractInsnNode.Jump_Insn);
        Types.Add(If_Icmple, AbstractInsnNode.Jump_Insn);
        Types.Add(If_Acmpeq, AbstractInsnNode.Jump_Insn);
        Types.Add(If_Acmpne, AbstractInsnNode.Jump_Insn);
        Types.Add(Goto, AbstractInsnNode.Jump_Insn);
        Types.Add(Jsr, AbstractInsnNode.Jump_Insn);
        Types.Add(Ifnull, AbstractInsnNode.Jump_Insn);
        Types.Add(Ifnonnull, AbstractInsnNode.Jump_Insn);
        Types.Add(Ldc, AbstractInsnNode.Ldc_Insn);
        Types.Add(Iinc, AbstractInsnNode.Iinc_Insn);
        Types.Add(Tableswitch, AbstractInsnNode.Tableswitch_Insn);
        Types.Add(Lookupswitch, AbstractInsnNode.Lookupswitch_Insn);
        Types.Add(Multianewarray, AbstractInsnNode.Multianewarray_Insn);

        #endregion
    }

    /// <summary>
    /// Creates a copy of an InsnList.
    /// </summary>
    /// <param name="insnList">The InsnList to copy.</param>
    /// <returns>A copy of the InsnList.</returns>
    public static InsnList Duplicate(this InsnList insnList)
    {
        MethodNode mv = new();
        insnList.Accept(mv);
        return mv.Instructions;
    }

    /// <summary>
    /// Returns a copy of this instruction, with LabelNodes still pointing to the original LabelNodes.
    /// </summary>
    /// <returns>a copy of this instruction. The returned instruction does not belong to any InsnList.</returns>
    public static AbstractInsnNode Clone(this AbstractInsnNode node) => node.Clone(new Dictionary<LabelNode, LabelNode>());

    /// <summary>
    /// Finds the method with the given signature.
    /// </summary>
    /// <param name="methodSignature">The method signature, in the format "your/Class's.method(Land/the/Params;)Lreturn/Type;"</param>
    /// <returns>The method node, or null if it could not be found.</returns>
    public static MethodNode FindMethod(string methodSignature)
    {
        MethodNode method = null;
        int indexOf = methodSignature.LastIndexOf('.');
        if (indexOf < 0)
            indexOf = methodSignature.IndexOf('<') - 1;
        string className = methodSignature[..indexOf];
        string methodNameAndDesc = methodSignature[(indexOf + 1)..];

        return FindClass(className).FindMethod(methodNameAndDesc);
    }
    
    /// <summary>
    /// Finds the method with the given signature.
    /// </summary>
    /// <param name="methodSignature">The method signature, in the format "method(Land/the/Params;)Lreturn/Type;"</param>
    /// <returns>The method node, or null if it could not be found.</returns>
    public static MethodNode FindMethod(this ClassNode node, string methodSignature)
    {
        MethodNode method = null;
        string methodName = methodSignature[..(methodSignature.IndexOf('('))];
        string methodDesc = methodSignature[methodSignature.IndexOf('(')..];

        foreach (MethodNode methodNode in node.Methods)
        {
            if (methodNode.Name != methodName)
                continue;
            if (methodNode.Desc != methodDesc && methodNode.Signature != methodDesc)
                continue;
            method = methodNode;
            break;
        }
        return method;
    }

    /// <summary>
    /// Finds the class with the given name.
    /// </summary>
    /// <param name="className">The name of the class to find.</param>
    /// <returns>The class node, or null if it could not be found.</returns>
    public static ClassNode FindClass(string className)
    {
        Classes.TryGetValue(className, out var v);
        return v.node;
    }
}