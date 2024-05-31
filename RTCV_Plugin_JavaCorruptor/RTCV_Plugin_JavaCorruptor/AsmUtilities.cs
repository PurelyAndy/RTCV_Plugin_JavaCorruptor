using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.IO.Compression;
using System.Reflection;
using NLog;
using ObjectWeb.Asm;
using ObjectWeb.Asm.Tree;
using static ObjectWeb.Asm.Opcodes;
using static Java_Corruptor.Constants;

namespace Java_Corruptor;

public static class AsmUtilities
{
    public static readonly BidirectionalDictionary<int, string> Opcodes = new(158);
    public static readonly BidirectionalDictionary<int, string> Tags = new(9);
    public static readonly Dictionary<int, int> Types = new(158);
    public static readonly Dictionary<string, ClassNode> Classes = [];
    
    private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

    static AsmUtilities()
    {
        foreach (FieldInfo field in typeof(Opcodes).GetFields(BindingFlags.Public | BindingFlags.Static))
        {
            if (field.Name.StartsWith("H_"))
                Tags.Add((int)field.GetValue(null), field.Name.ToUpper());
        }

        #region Add string versions of each opcode to the Opcodes dictionary.

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
        Opcodes.Add(F_New, "F_NEW");
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

        #region Add the type of instruction each opcode is for to the Types dictionary.

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
    /// Returns a copy of this instruction.
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
            indexOf = methodSignature.IndexOf('<');
        string className = methodSignature[..indexOf];
        string methodName = methodSignature.Substring(indexOf + 1, methodSignature.IndexOf('(') - indexOf - 1);
        string methodDesc = methodSignature[methodSignature.IndexOf('(')..];

        ClassNode node = FindClass(className);

        foreach (MethodNode methodNode in node.Methods)
        {
            if (methodNode.Name != methodName || methodNode.Desc != methodDesc)
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
        Classes.TryGetValue(className, out ClassNode node);
        return node;
    }

    //TODO: this is a wayyyy larger undertaking than i thought it would be. it needs to validate individual branches and stuff and that's just not happening right now.
    /// <summary>
    /// Validates the stack changes of an method's instructions.
    /// </summary>
    /// <param name="method">The method to validate.</param>
    /// <returns>The index of the first invalid instruction and the reason for it, or -1 if the stack is valid.</returns>
    public static (int Index, string Reason) ValidateStack(this MethodNode method)
    {
        Stack<JType> stack = new();
        AbstractInsnNode[] insns = method.Instructions.ToArray();
        ConstructorInfo ci = typeof(JType).GetConstructor(BindingFlags.NonPublic | BindingFlags.Instance, null, [typeof(int), typeof(string), typeof(int), typeof(int),
        ], null);
        for (int i = 0; i < insns.Length; i++)
        {
            AbstractInsnNode insn = insns[i];
            try
            {
                switch (insn.Opcode)
                {
                    case Nop:
                        break;
                    case Aconst_Null:
                    {
                        stack.Push((JType)ci.Invoke([JType.Object, "Ljava/lang/Object;", 0, 0]));
                        break;
                    }
                    case >= Iconst_M1 and <= Iconst_5:
                    {
                        stack.Push(JType.IntType);
                        break;
                    }
                    case <= Lconst_1:
                    {
                        stack.Push(JType.LongType);
                        break;
                    }
                    case <= Fconst_2:
                    {
                        stack.Push(JType.FloatType);
                        break;
                    }
                    case <= Dconst_1:
                    {
                        stack.Push(JType.DoubleType);
                        break;
                    }
                    case Bipush:
                    {
                        stack.Push(JType.ByteType);
                        break;
                    }
                    case Sipush:
                    {
                        stack.Push(JType.ShortType);
                        break;
                    }
                    case Ldc:
                    {
                        LdcInsnNode ldcInsnNode = (LdcInsnNode)insn;
                        if (ldcInsnNode.Cst is int)
                            stack.Push(JType.IntType);
                        else if (ldcInsnNode.Cst is long)
                            stack.Push(JType.LongType);
                        else if (ldcInsnNode.Cst is float)
                            stack.Push(JType.FloatType);
                        else if (ldcInsnNode.Cst is double)
                            stack.Push(JType.DoubleType);
                        else if (ldcInsnNode.Cst is string)
                            stack.Push((JType)ci.Invoke([JType.Object, "Ljava/lang/String;", 0, 0]));
                        else if (ldcInsnNode.Cst is Type)
                            stack.Push((JType)ci.Invoke([JType.Object, "Ljava/lang/Class;", 0, 0]));
                        else if (ldcInsnNode.Cst is Handle)
                            stack.Push((JType)ci.Invoke([JType.Object, "Ljava/lang/invoke/MethodHandle;", 0, 0]));
                        else if (ldcInsnNode.Cst is ConstantDynamic)
                            stack.Push((JType)ci.Invoke([JType.Object, "Ljava/lang/invoke/ConstantDynamic;", 0, 0]));
                        break;
                    }
                    case Iload:
                    {
                        VarInsnNode varInsnNode = (VarInsnNode)insn;
                        if (method.LocalVariables[varInsnNode.Var].Desc != "I")
                            return (i, "ILOAD must load an int, but is loading a " + method.LocalVariables[varInsnNode.Var].Desc);
                        stack.Push(JType.IntType);
                        break;
                    }
                    case Lload:
                    {
                        VarInsnNode varInsnNode = (VarInsnNode)insn;
                        if (method.LocalVariables[varInsnNode.Var].Desc != "J")
                            return (i, "LLOAD must load a long, but is loading a " + method.LocalVariables[varInsnNode.Var].Desc);
                        stack.Push(JType.LongType);
                        break;
                    }
                    case Fload:
                    {
                        VarInsnNode varInsnNode = (VarInsnNode)insn;
                        if (method.LocalVariables[varInsnNode.Var].Desc != "F")
                            return (i, "FLOAD must load a float, but is loading a " + method.LocalVariables[varInsnNode.Var].Desc);
                        stack.Push(JType.FloatType);
                        break;
                    }
                    case Dload:
                    {
                        VarInsnNode varInsnNode = (VarInsnNode)insn;
                        if (method.LocalVariables[varInsnNode.Var].Desc != "D")
                            return (i, "DLOAD must load a double, but is loading a " + method.LocalVariables[varInsnNode.Var].Desc);
                        stack.Push(JType.DoubleType);
                        break;
                    }
                    case Aload:
                    {
                        VarInsnNode varInsnNode = (VarInsnNode)insn;
                        stack.Push((JType)ci.Invoke([JType.Object, method.LocalVariables[varInsnNode.Var].Desc, 0, 0]));
                        break;
                    }
                    case >= Iload_0 and <= Iload_3:
                    {
                        int index = insn.Opcode - Iload_0;
                        if (method.LocalVariables[index].Desc != "I")
                            return (i, "ILOAD must load an int, but is loading a " + method.LocalVariables[index].Desc);
                        stack.Push(JType.IntType);
                        break;
                    }
                    case <= Lload_3:
                    {
                        int index = insn.Opcode - Lload_0;
                        if (method.LocalVariables[index].Desc != "J")
                            return (i, "LLOAD must load a long, but is loading a " + method.LocalVariables[index].Desc);
                        stack.Push(JType.LongType);
                        break;
                    }
                    case <= Fload_3:
                    {
                        int index = insn.Opcode - Fload_0;
                        if (method.LocalVariables[index].Desc != "F")
                            return (i, "FLOAD must load a float, but is loading a " + method.LocalVariables[index].Desc);
                        stack.Push(JType.FloatType);
                        break;
                    }
                    case <= Dload_3:
                    {
                        int index = insn.Opcode - Dload_0;
                        if (method.LocalVariables[index].Desc != "D")
                            return (i, "DLOAD must load a double, but is loading a " + method.LocalVariables[index].Desc);
                        stack.Push(JType.DoubleType);
                        break;
                    }
                    case <= Aload_3:
                    {
                        int index = insn.Opcode - Aload_0;
                        stack.Push((JType)ci.Invoke([JType.Object, method.LocalVariables[index].Desc, 0, 0]));
                        break;
                    }
                    case Iaload:
                    {
                        JType type = stack.Pop();
                        if (type.Sort != JType.Int)
                            return (i, "Array index must be an int, but is a " + type.Sort);
                        stack.Pop();
                        stack.Push(JType.IntType);
                        break;
                    }
                    case Laload:
                    {
                        JType type = stack.Pop();
                        if (type.Sort != JType.Int)
                            return (i, "Array index must be an int, but is a " + type.Sort);
                        stack.Pop();
                        stack.Push(JType.LongType);
                        break;
                    }
                    case Faload:
                    {
                        JType type = stack.Pop();
                        if (type.Sort != JType.Int)
                            return (i, "Array index must be an int, but is a " + type.Sort);
                        stack.Pop();
                        stack.Push(JType.FloatType);
                        break;
                    }
                    case Daload:
                    {
                        JType type = stack.Pop();
                        if (type.Sort != JType.Int)
                            return (i, "Array index must be an int, but is a " + type.Sort);
                        stack.Pop();
                        stack.Push(JType.DoubleType);
                        break;
                    }
                    case Aaload:
                    {
                        JType type = stack.Pop();
                        if (type.Sort != JType.Int)
                            return (i, "Array index must be an int, but is a " + type.Sort);
                        stack.Pop();
                        stack.Push((JType)ci.Invoke([JType.Object, "Ljava/lang/Object;", 0, 0]));
                        break;
                    }
                    case Baload:
                    {
                        JType type = stack.Pop();
                        if (type.Sort != JType.Int)
                            return (i, "Array index must be an int, but is a " + type.Sort);
                        stack.Pop();
                        stack.Push(JType.ByteType);
                        break;
                    }
                    case Caload:
                    {
                        JType type = stack.Pop();
                        if (type.Sort != JType.Int)
                            return (i, "Array index must be an int, but is a " + type.Sort);
                        stack.Pop();
                        stack.Push(JType.CharType);
                        break;
                    }
                    case Saload:
                    {
                        JType type = stack.Pop();
                        if (type.Sort != JType.Int)
                            return (i, "Array index must be an int, but is a " + type.Sort);
                        stack.Pop();
                        stack.Push(JType.ShortType);
                        break;
                    }
                    case Istore:
                    {
                        VarInsnNode varInsnNode = (VarInsnNode)insn;
                        JType type = stack.Pop();
                        if (type.Sort != JType.Int)
                            return (i, "ISTORE must store an int, but is storing a " + type.Sort);
                        if (method.LocalVariables[varInsnNode.Var].Desc != "I")
                            return (i, "ISTORE must store to an int, but is storing to a " + method.LocalVariables[varInsnNode.Var].Desc);
                        break;
                    }
                    case Lstore:
                    {
                        VarInsnNode varInsnNode = (VarInsnNode)insn;
                        JType type = stack.Pop();
                        if (type.Sort != JType.Long)
                            return (i, "LSTORE must store a long, but is storing a " + type.Sort);
                        if (method.LocalVariables[varInsnNode.Var].Desc != "J")
                            return (i, "LSTORE must store to a long, but is storing to a " + method.LocalVariables[varInsnNode.Var].Desc);
                        break;
                    }
                    case Fstore:
                    {
                        VarInsnNode varInsnNode = (VarInsnNode)insn;
                        JType type = stack.Pop();
                        if (type.Sort != JType.Float)
                            return (i, "FSTORE must store a float, but is storing a " + type.Sort);
                        if (method.LocalVariables[varInsnNode.Var].Desc != "F")
                            return (i, "FSTORE must store to a float, but is storing to a " + method.LocalVariables[varInsnNode.Var].Desc);
                        break;
                    }
                    case Dstore:
                    {
                        VarInsnNode varInsnNode = (VarInsnNode)insn;
                        JType type = stack.Pop();
                        if (type.Sort != JType.Double)
                            return (i, "DSTORE must store a double, but is storing a " + type.Sort);
                        if (method.LocalVariables[varInsnNode.Var].Desc != "D")
                            return (i, "DSTORE must store to a double, but is storing to a " + method.LocalVariables[varInsnNode.Var].Desc);
                        break;
                    }
                    case Astore:
                    {
                        VarInsnNode varInsnNode = (VarInsnNode)insn;
                        JType type = stack.Pop();
                        if (type.Sort != JType.Object)
                            return (i, "ASTORE must store an object, but is storing a " + type.Sort);
                        if (method.LocalVariables[varInsnNode.Var].Desc != type.Descriptor)
                            return (i,
                                $"Variable type {method.LocalVariables[varInsnNode.Var].Desc} does not match stack type {type.Descriptor}");
                        break;
                    }
                    case <= Istore_3:
                    {
                        int index = insn.Opcode - Istore_0;
                        JType type = stack.Pop();
                        if (type.Sort != JType.Int)
                            return (i, "ISTORE must store an int, but is storing a " + type.Sort);
                        if (method.LocalVariables[index].Desc != "I")
                            return (i, "ISTORE must store to an int, but is storing to a " + method.LocalVariables[index].Desc);
                        break;
                    }
                    case <= Lstore_3:
                    {
                        int index = insn.Opcode - Lstore_0;
                        JType type = stack.Pop();
                        if (type.Sort != JType.Long)
                            return (i, "LSTORE must store a long, but is storing a " + type.Sort);
                        if (method.LocalVariables[index].Desc != "J")
                            return (i, "LSTORE must store to a long, but is storing to a " + method.LocalVariables[index].Desc);
                        break;
                    }
                    case <= Fstore_3:
                    {
                        int index = insn.Opcode - Fstore_0;
                        JType type = stack.Pop();
                        if (type.Sort != JType.Float)
                            return (i, "FSTORE must store a float, but is storing a " + type.Sort);
                        if (method.LocalVariables[index].Desc != "F")
                            return (i, "FSTORE must store to a float, but is storing to a " + method.LocalVariables[index].Desc);
                        break;
                    }
                    case <= Dstore_3:
                    {
                        int index = insn.Opcode - Dstore_0;
                        JType type = stack.Pop();
                        if (type.Sort != JType.Double)
                            return (i, "DSTORE must store a double, but is storing a " + type.Sort);
                        if (method.LocalVariables[index].Desc != "D")
                            return (i, "DSTORE must store to a double, but is storing to a " + method.LocalVariables[index].Desc);
                        break;
                    }
                    case <= Astore_3:
                    {
                        int index = insn.Opcode - Astore_0;
                        JType type = stack.Pop();
                        if (type.Sort != JType.Object)
                            return (i, "ASTORE must store an object, but is storing a " + type.Sort);
                        if (method.LocalVariables[index].Desc != type.Descriptor)
                            return (i,
                                $"Variable type {method.LocalVariables[index].Desc} does not match stack type {type.Descriptor}");
                        break;
                    }
                    case Iastore:
                    {
                        JType type1 = stack.Pop();
                        if (type1.Sort != JType.Int)
                            return (i, "IASTORE must store an int, but is storing a " + type1.Sort);
                        JType type2 = stack.Pop();
                        if (type2.Sort != JType.Int)
                            return (i, "Array index must be an int, but is a " + type2.Sort);
                        stack.Pop();
                        break;
                    }
                    case Lastore:
                    {
                        JType type1 = stack.Pop();
                        if (type1.Sort != JType.Long)
                            return (i, "LASTORE must store a long, but is storing a " + type1.Sort);
                        JType type2 = stack.Pop();
                        if (type2.Sort != JType.Int)
                            return (i, "Array index must be an int, but is a " + type2.Sort);
                        stack.Pop();
                        break;
                    }
                    case Fastore:
                    {
                        JType type1 = stack.Pop();
                        if (type1.Sort != JType.Float)
                            return (i, "FASTORE must store a float, but is storing a " + type1.Sort);
                        JType type2 = stack.Pop();
                        if (type2.Sort != JType.Int)
                            return (i, "Array index must be an int, but is a " + type2.Sort);
                        stack.Pop();
                        break;
                    }
                    case Dastore:
                    {
                        JType type1 = stack.Pop();
                        if (type1.Sort != JType.Double)
                            return (i, "DASTORE must store a double, but is storing a " + type1.Sort);
                        JType type2 = stack.Pop();
                        if (type2.Sort != JType.Int)
                            return (i, "Array index must be an int, but is a " + type2.Sort);
                        stack.Pop();
                        break;
                    }
                    case Aastore:
                    {
                        JType type1 = stack.Pop();
                        if (type1.Sort != JType.Object)
                            return (i, "AASTORE must store an object, but is storing a " + type1.Sort);
                        JType type2 = stack.Pop();
                        if (type2.Sort != JType.Int)
                            return (i, "Array index must be an int, but is a " + type2.Sort);
                        JType type3 = stack.Pop();
                        if (type3.Sort != JType.Object)
                            return (i, "Array must be an object array, but is a " + type3.Sort);
                        break;
                    }
                    case Bastore:
                    {
                        JType type1 = stack.Pop();
                        if (type1.Sort != JType.Byte)
                            return (i, "BASTORE must store a byte, but is storing a " + type1.Sort);
                        JType type2 = stack.Pop();
                        if (type2.Sort != JType.Int)
                            return (i, "Array index must be an int, but is a " + type2.Sort);
                        stack.Pop();
                        break;
                    }
                    case Castore:
                    {
                        JType type1 = stack.Pop();
                        if (type1.Sort != JType.Char)
                            return (i, "CASTORE must store a char, but is storing a " + type1.Sort);
                        JType type2 = stack.Pop();
                        if (type2.Sort != JType.Int)
                            return (i, "Array index must be an int, but is a " + type2.Sort);
                        stack.Pop();
                        break;
                    }
                    case Sastore:
                    {
                        JType type1 = stack.Pop();
                        if (type1.Sort != JType.Short)
                            return (i, "SASTORE must store a short, but is storing a " + type1.Sort);
                        JType type2 = stack.Pop();
                        if (type2.Sort != JType.Int)
                            return (i, "Array index must be an int, but is a " + type2.Sort);
                        stack.Pop();
                        break;
                    }
                    case Pop:
                    {
                        stack.Pop();
                        break;
                    }
                    case Pop2:
                    {
                        JType type = stack.Pop();
                        if (type.Size == 1)
                            stack.Pop();
                        break;
                    }
                    case Dup:
                    {
                        JType type = stack.Pop();
                        stack.Push(type);
                        stack.Push(type);
                        break;
                    }
                    case Dup_X1:
                    {
                        JType type1 = stack.Pop();
                        JType type2 = stack.Pop();
                        stack.Push(type1);
                        stack.Push(type2);
                        stack.Push(type1);
                        break;
                    }
                    case Dup_X2:
                    {
                        JType type1 = stack.Pop();
                        JType type2 = stack.Pop();
                        JType type3 = stack.Pop();
                        stack.Push(type1);
                        stack.Push(type3);
                        stack.Push(type2);
                        stack.Push(type1);
                        break;
                    }
                    case Dup2:
                    {
                        JType type1 = stack.Pop();
                        if (type1.Size == 2)
                        {
                            stack.Push(type1);
                            stack.Push(type1);
                        }
                        else
                        {
                            JType type2 = stack.Pop();
                            stack.Push(type2);
                            stack.Push(type1);
                            stack.Push(type2);
                            stack.Push(type1);
                        }
                        break;
                    }
                    case Dup2_X1:
                    {
                        JType type1 = stack.Pop();
                        if (type1.Size == 2)
                        {
                            JType type2 = stack.Pop();
                            stack.Push(type1);
                            stack.Push(type2);
                            stack.Push(type1);
                        }
                        else
                        {
                            JType type2 = stack.Pop();
                            JType type3 = stack.Pop();
                            stack.Push(type2);
                            stack.Push(type1);
                            stack.Push(type3);
                            stack.Push(type2);
                            stack.Push(type1);
                        }
                        break;
                    }
                    case Dup2_X2:
                    {
                        JType type1 = stack.Pop();
                        if (type1.Size == 2)
                        {
                            JType type2 = stack.Pop();
                            if (type2.Size == 2)
                            {
                                stack.Push(type1);
                                stack.Push(type2);
                                stack.Push(type1);
                            }
                            else
                            {
                                JType type3 = stack.Pop();
                                stack.Push(type1);
                                stack.Push(type3);
                                stack.Push(type2);
                                stack.Push(type1);
                            }
                        }
                        else
                        {
                            JType type2 = stack.Pop();
                            JType type3 = stack.Pop();
                            if (type3.Size == 2)
                            {
                                stack.Push(type2);
                                stack.Push(type1);
                                stack.Push(type3);
                                stack.Push(type2);
                                stack.Push(type1);
                            }
                            else
                            {
                                JType type4 = stack.Pop();
                                stack.Push(type2);
                                stack.Push(type1);
                                stack.Push(type4);
                                stack.Push(type3);
                                stack.Push(type2);
                                stack.Push(type1);
                            }
                        }
                        break;
                    }
                    // i really hope i'm handling those dup instructions correctly
                    case Swap:
                    {
                        JType type1 = stack.Pop();
                        JType type2 = stack.Pop();
                        stack.Push(type1);
                        stack.Push(type2);
                        break;
                    }
                    case Iadd:
                    {
                        JType type1 = stack.Pop();
                        if (type1.Sort != JType.Int)
                            return (i, "IADD must add two ints, but the 2nd value is of type " + type1.Descriptor);
                        JType type2 = stack.Pop();
                        if (type2.Sort != JType.Int)
                            return (i, "IADD must add two ints, but the 1st value is of type " + type2.Descriptor);
                        stack.Push(JType.IntType);
                        break;
                    }
                    case Ladd:
                    {
                        JType type1 = stack.Pop();
                        if (type1.Sort != JType.Long)
                            return (i, "LADD must add two longs, but the 2nd value is of type " + type1.Descriptor);
                        JType type2 = stack.Pop();
                        if (type2.Sort != JType.Long)
                            return (i, "LADD must add two longs, but the 1st value is of type " + type2.Descriptor);
                        stack.Push(JType.LongType);
                        break;
                    }
                    case Fadd:
                    {
                        JType type1 = stack.Pop();
                        if (type1.Sort != JType.Float)
                            return (i, "FADD must add two floats, but the 2nd value is of type " + type1.Descriptor);
                        JType type2 = stack.Pop();
                        if (type2.Sort != JType.Float)
                            return (i, "FADD must add two floats, but the 1st value is of type " + type2.Descriptor);
                        stack.Push(JType.FloatType);
                        break;
                    }
                    case Dadd:
                    {
                        JType type1 = stack.Pop();
                        if (type1.Sort != JType.Double)
                            return (i, "DADD must add two doubles, but the 2nd value is of type " + type1.Descriptor);
                        JType type2 = stack.Pop();
                        if (type2.Sort != JType.Double)
                            return (i, "DADD must add two doubles, but the 1st value is of type " + type2.Descriptor);
                        stack.Push(JType.DoubleType);
                        break;
                    }
                    case Isub:
                    {
                        JType type1 = stack.Pop();
                        if (type1.Sort != JType.Int)
                            return (i, "ISUB must subtract two ints, but the 2nd value is of type " + type1.Descriptor);
                        JType type2 = stack.Pop();
                        if (type2.Sort != JType.Int)
                            return (i, "ISUB must subtract two ints, but the 1st value is of type " + type2.Descriptor);
                        stack.Push(JType.IntType);
                        break;
                    }
                    case Lsub:
                    {
                        JType type1 = stack.Pop();
                        if (type1.Sort != JType.Long)
                            return (i, "LSUB must subtract two longs, but the 2nd value is of type " + type1.Descriptor);
                        JType type2 = stack.Pop();
                        if (type2.Sort != JType.Long)
                            return (i, "LSUB must subtract two longs, but the 1st value is of type " + type2.Descriptor);
                        stack.Push(JType.LongType);
                        break;
                    }
                    case Fsub:
                    {
                        JType type1 = stack.Pop();
                        if (type1.Sort != JType.Float)
                            return (i, "FSUB must subtract two floats, but the 2nd value is of type " + type1.Descriptor);
                        JType type2 = stack.Pop();
                        if (type2.Sort != JType.Float)
                            return (i, "FSUB must subtract two floats, but the 1st value is of type " + type2.Descriptor);
                        stack.Push(JType.FloatType);
                        break;
                    }
                    case Dsub:
                    {
                        JType type1 = stack.Pop();
                        if (type1.Sort != JType.Double)
                            return (i, "DSUB must subtract two doubles, but the 2nd value is of type " + type1.Descriptor);
                        JType type2 = stack.Pop();
                        if (type2.Sort != JType.Double)
                            return (i, "DSUB must subtract two doubles, but the 1st value is of type " + type2.Descriptor);
                        stack.Push(JType.DoubleType);
                        break;
                    }
                    case Imul:
                    {
                        JType type1 = stack.Pop();
                        if (type1.Sort != JType.Int)
                            return (i, "IMUL must multiply two ints, but the 2nd value is of type " + type1.Descriptor);
                        JType type2 = stack.Pop();
                        if (type2.Sort != JType.Int)
                            return (i, "IMUL must multiply two ints, but the 1st value is of type " + type2.Descriptor);
                        stack.Push(JType.IntType);
                        break;
                    }
                    case Lmul:
                    {
                        JType type1 = stack.Pop();
                        if (type1.Sort != JType.Long)
                            return (i, "LMUL must multiply two longs, but the 2nd value is of type " + type1.Descriptor);
                        JType type2 = stack.Pop();
                        if (type2.Sort != JType.Long)
                            return (i, "LMUL must multiply two longs, but the 1st value is of type " + type2.Descriptor);
                        stack.Push(JType.LongType);
                        break;
                    }
                    case Fmul:
                    {
                        JType type1 = stack.Pop();
                        if (type1.Sort != JType.Float)
                            return (i, "FMUL must multiply two floats, but the 2nd value is of type " + type1.Descriptor);
                        JType type2 = stack.Pop();
                        if (type2.Sort != JType.Float)
                            return (i, "FMUL must multiply two floats, but the 1st value is of type " + type2.Descriptor);
                        stack.Push(JType.FloatType);
                        break;
                    }
                    case Dmul:
                    {
                        JType type1 = stack.Pop();
                        if (type1.Sort != JType.Double)
                            return (i, "DMUL must multiply two doubles, but the 2nd value is of type " + type1.Descriptor);
                        JType type2 = stack.Pop();
                        if (type2.Sort != JType.Double)
                            return (i, "DMUL must multiply two doubles, but the 1st value is of type " + type2.Descriptor);
                        stack.Push(JType.DoubleType);
                        break;
                    }
                    case Idiv:
                    {
                        JType type1 = stack.Pop();
                        if (type1.Sort != JType.Int)
                            return (i, "IDIV must divide two ints, but the 2nd value (the divisor) is of type " + type1.Descriptor);
                        JType type2 = stack.Pop();
                        if (type2.Sort != JType.Int)
                            return (i, "IDIV must divide two ints, but the 1st value (the dividend) is of type " + type2.Descriptor);
                        stack.Push(JType.IntType);
                        break;
                    }
                    case Ldiv:
                    {
                        JType type1 = stack.Pop();
                        if (type1.Sort != JType.Long)
                            return (i, "LDIV must divide two longs, but the 2nd value (the divisor) is of type " + type1.Descriptor);
                        JType type2 = stack.Pop();
                        if (type2.Sort != JType.Long)
                            return (i, "LDIV must divide two longs, but the 1st value (the dividend) is of type " + type2.Descriptor);
                        stack.Push(JType.LongType);
                        break;
                    }
                    case Fdiv:
                    {
                        JType type1 = stack.Pop();
                        if (type1.Sort != JType.Float)
                            return (i, "FDIV must divide two floats, but the 2nd value (the divisor) is of type " + type1.Descriptor);
                        JType type2 = stack.Pop();
                        if (type2.Sort != JType.Float)
                            return (i, "FDIV must divide two floats, but the 1st value (the dividend) is of type " + type2.Descriptor);
                        stack.Push(JType.FloatType);
                        break;
                    }
                    case Ddiv:
                    {
                        JType type1 = stack.Pop();
                        if (type1.Sort != JType.Double)
                            return (i, "DDIV must divide two doubles, but the 2nd value (the divisor) is of type " + type1.Descriptor);
                        JType type2 = stack.Pop();
                        if (type2.Sort != JType.Double)
                            return (i, "DDIV must divide two doubles, but the 1st value (the dividend) is of type " + type2.Descriptor);
                        stack.Push(JType.DoubleType);
                        break;
                    }
                    case Irem:
                    {
                        JType type1 = stack.Pop();
                        if (type1.Sort != JType.Int)
                            return (i, "IREM must calculate the remainder of the division of two ints, but the 2nd value (the divisor) is of type " + type1.Descriptor);
                        JType type2 = stack.Pop();
                        if (type2.Sort != JType.Int)
                            return (i, "IREM must calculate the remainder of the division of two ints, but the 1st value (the dividend) is of type " + type2.Descriptor);
                        stack.Push(JType.IntType);
                        break;
                    }
                    case Lrem:
                    {
                        JType type1 = stack.Pop();
                        if (type1.Sort != JType.Long)
                            return (i, "LREM must calculate the remainder of the division of two longs, but the 2nd value (the divisor) is of type " + type1.Descriptor);
                        JType type2 = stack.Pop();
                        if (type2.Sort != JType.Long)
                            return (i, "LREM must calculate the remainder of the division of two longs, but the 1st value (the dividend) is of type " + type2.Descriptor);
                        stack.Push(JType.LongType);
                        break;
                    }
                    case Frem:
                    {
                        JType type1 = stack.Pop();
                        if (type1.Sort != JType.Float)
                            return (i, "FREM must calculate the remainder of the division of two floats, but the 2nd value (the divisor) is of type " + type1.Descriptor);
                        JType type2 = stack.Pop();
                        if (type2.Sort != JType.Float)
                            return (i, "FREM must calculate the remainder of the division of two floats, but the 1st value (the dividend) is of type " + type2.Descriptor);
                        stack.Push(JType.FloatType);
                        break;
                    }
                    case Drem:
                    {
                        JType type1 = stack.Pop();
                        if (type1.Sort != JType.Double)
                            return (i, "DREM must calculate the remainder of the division of two doubles, but the 2nd value (the divisor) is of type " + type1.Descriptor);
                        JType type2 = stack.Pop();
                        if (type2.Sort != JType.Double)
                            return (i, "DREM must calculate the remainder of the division of two doubles, but the 1st value (the dividend) is of type " + type2.Descriptor);
                        stack.Push(JType.DoubleType);
                        break;
                    }
                    case Ineg:
                    {
                        JType type = stack.Pop();
                        if (type.Sort != JType.Int)
                            return (i, "INEG must negate an int, but is negating a " + type.Sort);
                        stack.Push(JType.IntType);
                        break;
                    }
                    case Lneg:
                    {
                        JType type = stack.Pop();
                        if (type.Sort != JType.Long)
                            return (i, "LNEG must negate a long, but is negating a " + type.Sort);
                        stack.Push(JType.LongType);
                        break;
                    }
                    case Fneg:
                    {
                        JType type = stack.Pop();
                        if (type.Sort != JType.Float)
                            return (i, "FNEG must negate a float, but is negating a " + type.Sort);
                        stack.Push(JType.FloatType);
                        break;
                    }
                    case Dneg:
                    {
                        JType type = stack.Pop();
                        if (type.Sort != JType.Double)
                            return (i, "DNEG must negate a double, but is negating a " + type.Sort);
                        stack.Push(JType.DoubleType);
                        break;
                    }
                    case Ishl:
                    {
                        JType type1 = stack.Pop();
                        if (type1.Sort != JType.Int)
                            return (i, "ISHL must shift by an int, but is shifting by a " + type1.Sort);
                        JType type2 = stack.Pop();
                        if (type2.Sort != JType.Int)
                            return (i, "ISHL must shift an int, but the value is of type " + type2.Descriptor);
                        stack.Push(JType.IntType);
                        break;
                    }
                    case Lshl:
                    {
                        JType type1 = stack.Pop();
                        if (type1.Sort != JType.Int)
                            return (i, "LSHL must shift by an int, but is shifting by a " + type1.Sort);
                        JType type2 = stack.Pop();
                        if (type2.Sort != JType.Long)
                            return (i, "LSHL must shift a long, but the value is of type " + type2.Descriptor);
                        stack.Push(JType.LongType);
                        break;
                    }
                    case Ishr:
                    {
                        JType type1 = stack.Pop();
                        if (type1.Sort != JType.Int)
                            return (i, "ISHR must shift by an int, but is shifting by a " + type1.Sort);
                        JType type2 = stack.Pop();
                        if (type2.Sort != JType.Int)
                            return (i, "ISHR must shift an int, but the value is of type " + type2.Descriptor);
                        stack.Push(JType.IntType);
                        break;
                    }
                    case Lshr:
                    {
                        JType type1 = stack.Pop();
                        if (type1.Sort != JType.Int)
                            return (i, "LSHR must shift by an int, but is shifting by a " + type1.Sort);
                        JType type2 = stack.Pop();
                        if (type2.Sort != JType.Long)
                            return (i, "LSHR must shift a long, but the value is of type " + type2.Descriptor);
                        stack.Push(JType.LongType);
                        break;
                    }
                    case Iushr:
                    {
                        JType type1 = stack.Pop();
                        if (type1.Sort != JType.Int)
                            return (i, "IUSHR must shift by an int, but is shifting by a " + type1.Sort);
                        JType type2 = stack.Pop();
                        if (type2.Sort != JType.Int)
                            return (i, "IUSHR must shift an int, but the value is of type " + type2.Descriptor);
                        stack.Push(JType.IntType);
                        break;
                    }
                    case Lushr:
                    {
                        JType type1 = stack.Pop();
                        if (type1.Sort != JType.Int)
                            return (i, "LUSHR must shift by an int, but is shifting by a " + type1.Sort);
                        JType type2 = stack.Pop();
                        if (type2.Sort != JType.Long)
                            return (i, "LUSHR must shift a long, but the value is of type " + type2.Descriptor);
                        stack.Push(JType.LongType);
                        break;
                    }
                    case Iand:
                    {
                        JType type1 = stack.Pop();
                        if (type1.Sort != JType.Int)
                            return (i, "IAND must AND two ints, but the 2nd value is of type " + type1.Descriptor);
                        JType type2 = stack.Pop();
                        if (type2.Sort != JType.Int)
                            return (i, "IAND must AND two ints, but the 1st value is of type " + type2.Descriptor);
                        stack.Push(JType.IntType);
                        break;
                    }
                    case Land:
                    {
                        JType type1 = stack.Pop();
                        if (type1.Sort != JType.Long)
                            return (i, "LAND must AND two longs, but the 2nd value is of type " + type1.Descriptor);
                        JType type2 = stack.Pop();
                        if (type2.Sort != JType.Long)
                            return (i, "LAND must AND two longs, but the 1st value is of type " + type2.Descriptor);
                        stack.Push(JType.LongType);
                        break;
                    }
                    case Ior:
                    {
                        JType type1 = stack.Pop();
                        if (type1.Sort != JType.Int)
                            return (i, "IOR must OR two ints, but the 2nd value is of type " + type1.Descriptor);
                        JType type2 = stack.Pop();
                        if (type2.Sort != JType.Int)
                            return (i, "IOR must OR two ints, but the 1st value is of type " + type2.Descriptor);
                        stack.Push(JType.IntType);
                        break;
                    }
                    case Lor:
                    {
                        JType type1 = stack.Pop();
                        if (type1.Sort != JType.Long)
                            return (i, "LOR must OR two longs, but the 2nd value is of type " + type1.Descriptor);
                        JType type2 = stack.Pop();
                        if (type2.Sort != JType.Long)
                            return (i, "LOR must OR two longs, but the 1st value is of type " + type2.Descriptor);
                        stack.Push(JType.LongType);
                        break;
                    }
                    case Ixor:
                    {
                        JType type1 = stack.Pop();
                        if (type1.Sort != JType.Int)
                            return (i, "IXOR must XOR two ints, but the 2nd value is of type " + type1.Descriptor);
                        JType type2 = stack.Pop();
                        if (type2.Sort != JType.Int)
                            return (i, "IXOR must XOR two ints, but the 1st value is of type " + type2.Descriptor);
                        stack.Push(JType.IntType);
                        break;
                    }
                    case Lxor:
                    {
                        JType type1 = stack.Pop();
                        if (type1.Sort != JType.Long)
                            return (i, "LXOR must XOR two longs, but the 2nd value is of type " + type1.Descriptor);
                        JType type2 = stack.Pop();
                        if (type2.Sort != JType.Long)
                            return (i, "LXOR must XOR two longs, but the 1st value is of type " + type2.Descriptor);
                        stack.Push(JType.LongType);
                        break;
                    }
                    case Iinc:
                    {
                        IincInsnNode iincInsnNode = (IincInsnNode)insn;
                        if (method.LocalVariables[iincInsnNode.Var].Desc != "I")
                            return (i, "IINC must increment an int, but is incrementing a " + method.LocalVariables[iincInsnNode.Var].Desc);
                        break;
                    }
                    case I2L:
                    {
                        JType type = stack.Pop();
                        if (type.Sort != JType.Int)
                            return (i, "I2L must convert an int to a long, but is converting a " + type.Sort);
                        stack.Push(JType.LongType);
                        break;
                    }
                    case I2F:
                    {
                        JType type = stack.Pop();
                        if (type.Sort != JType.Int)
                            return (i, "I2F must convert an int to a float, but is converting a " + type.Sort);
                        stack.Push(JType.FloatType);
                        break;
                    }
                    case I2D:
                    {
                        JType type = stack.Pop();
                        if (type.Sort != JType.Int)
                            return (i, "I2D must convert an int to a double, but is converting a " + type.Sort);
                        stack.Push(JType.DoubleType);
                        break;
                    }
                    case L2I:
                    {
                        JType type = stack.Pop();
                        if (type.Sort != JType.Long)
                            return (i, "L2I must convert a long to an int, but is converting a " + type.Sort);
                        stack.Push(JType.IntType);
                        break;
                    }
                    case L2F:
                    {
                        JType type = stack.Pop();
                        if (type.Sort != JType.Long)
                            return (i, "L2F must convert a long to a float, but is converting a " + type.Sort);
                        stack.Push(JType.FloatType);
                        break;
                    }
                    case L2D:
                    {
                        JType type = stack.Pop();
                        if (type.Sort != JType.Long)
                            return (i, "L2D must convert a long to a double, but is converting a " + type.Sort);
                        stack.Push(JType.DoubleType);
                        break;
                    }
                    case F2I:
                    {
                        JType type = stack.Pop();
                        if (type.Sort != JType.Float)
                            return (i, "F2I must convert a float to an int, but is converting a " + type.Sort);
                        stack.Push(JType.IntType);
                        break;
                    }
                    case F2L:
                    {
                        JType type = stack.Pop();
                        if (type.Sort != JType.Float)
                            return (i, "F2L must convert a float to a long, but is converting a " + type.Sort);
                        stack.Push(JType.LongType);
                        break;
                    }
                    case F2D:
                    {
                        JType type = stack.Pop();
                        if (type.Sort != JType.Float)
                            return (i, "F2D must convert a float to a double, but is converting a " + type.Sort);
                        stack.Push(JType.DoubleType);
                        break;
                    }
                    case D2I:
                    {
                        JType type = stack.Pop();
                        if (type.Sort != JType.Double)
                            return (i, "D2I must convert a double to an int, but is converting a " + type.Sort);
                        stack.Push(JType.IntType);
                        break;
                    }
                    case D2L:
                    {
                        JType type = stack.Pop();
                        if (type.Sort != JType.Double)
                            return (i, "D2L must convert a double to a long, but is converting a " + type.Sort);
                        stack.Push(JType.LongType);
                        break;
                    }
                    case D2F:
                    {
                        JType type = stack.Pop();
                        if (type.Sort != JType.Double)
                            return (i, "D2F must convert a double to a float, but is converting a " + type.Sort);
                        stack.Push(JType.FloatType);
                        break;
                    }
                    case I2B:
                    {
                        JType type = stack.Pop();
                        if (type.Sort != JType.Int)
                            return (i, "I2B must convert an int to a byte, but is converting a " + type.Sort);
                        stack.Push(JType.ByteType);
                        break;
                    }
                    case I2C:
                    {
                        JType type = stack.Pop();
                        if (type.Sort != JType.Int)
                            return (i, "I2C must convert an int to a char, but is converting a " + type.Sort);
                        stack.Push(JType.CharType);
                        break;
                    }
                    case I2S:
                    {
                        JType type = stack.Pop();
                        if (type.Sort != JType.Int)
                            return (i, "I2S must convert an int to a short, but is converting a " + type.Sort);
                        stack.Push(JType.ShortType);
                        break;
                    }
                    case Lcmp:
                    {
                        JType type1 = stack.Pop();
                        if (type1.Sort != JType.Long)
                            return (i, "LCMP must compare two longs, but the 2nd value is of type " + type1.Descriptor);
                        JType type2 = stack.Pop();
                        if (type2.Sort != JType.Long)
                            return (i, "LCMP must compare two longs, but the 1st value is of type " + type2.Descriptor);
                        stack.Push(JType.IntType);
                        break;
                    }
                    case Fcmpl:
                    {
                        JType type1 = stack.Pop();
                        if (type1.Sort != JType.Float)
                            return (i, "FCMPL must compare two floats, but the 2nd value is of type " + type1.Descriptor);
                        JType type2 = stack.Pop();
                        if (type2.Sort != JType.Float)
                            return (i, "FCMPL must compare two floats, but the 1st value is of type " + type2.Descriptor);
                        stack.Push(JType.IntType);
                        break;
                    }
                    case Fcmpg:
                    {
                        JType type1 = stack.Pop();
                        if (type1.Sort != JType.Float)
                            return (i, "FCMPG must compare two floats, but the 2nd value is of type " + type1.Descriptor);
                        JType type2 = stack.Pop();
                        if (type2.Sort != JType.Float)
                            return (i, "FCMPG must compare two floats, but the 1st value is of type " + type2.Descriptor);
                        stack.Push(JType.IntType);
                        break;
                    }
                    case Dcmpl:
                    {
                        JType type1 = stack.Pop();
                        if (type1.Sort != JType.Double)
                            return (i, "DCMPL must compare two doubles, but the 2nd value is of type " + type1.Descriptor);
                        JType type2 = stack.Pop();
                        if (type2.Sort != JType.Double)
                            return (i, "DCMPL must compare two doubles, but the 1st value is of type " + type2.Descriptor);
                        stack.Push(JType.IntType);
                        break;
                    }
                    case Dcmpg:
                    {
                        JType type1 = stack.Pop();
                        if (type1.Sort != JType.Double)
                            return (i, "DCMPG must compare two doubles, but the 2nd value is of type " + type1.Descriptor);
                        JType type2 = stack.Pop();
                        if (type2.Sort != JType.Double)
                            return (i, "DCMPG must compare two doubles, but the 1st value is of type " + type2.Descriptor);
                        stack.Push(JType.IntType);
                        break;
                    }
                }   
            }
            catch (InvalidOperationException)
            {
                return (i, "Cannot pop from an empty stack");
            }
            catch (Exception e)
            {
                return (i, e.Message);
            }
        }
        
        return (-1, null);
    }

    public static void LoadClasses(ZipArchive zipArchive)
    {
        Classes.Clear();
            
        foreach (ZipArchiveEntry zipArchiveEntry in zipArchive.Entries)
            if (zipArchiveEntry.FullName.EndsWith(".class"))
            {
                using Stream stream = zipArchiveEntry.Open();
                byte[] classBytes = new byte[zipArchiveEntry.Length];
                int bytesRead = 0;
                do
                    bytesRead += stream.Read(classBytes, bytesRead, classBytes.Length - bytesRead);
                while (bytesRead < classBytes.Length);

                ClassReader classReader = new((sbyte[])(Array)classBytes);
                ClassNode classNode = new();
                classReader.Accept(classNode, 0);

                Classes.Add(classNode.Name, classNode);
            }
    }
}