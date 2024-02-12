package andy;

import com.google.common.collect.HashBiMap;
import org.objectweb.asm.Opcodes;
import org.objectweb.asm.tree.AbstractInsnNode;
import org.objectweb.asm.tree.ClassNode;
import org.objectweb.asm.tree.InsnList;
import org.objectweb.asm.tree.MethodNode;

import java.lang.reflect.Field;
import java.util.ArrayList;
import java.util.HashMap;
import java.util.Map;

import static org.objectweb.asm.Opcodes.*;

//This class copies some code from the ASM library.
public class AsmUtilities {
    public static HashBiMap<Integer, String> opcodes = HashBiMap.create(158);
    public static HashBiMap<Integer, String> tags = HashBiMap.create();
    public static Map<Integer, Integer> types = new HashMap<>(158);

    public static ArrayList<ClassNode> classes = new ArrayList<>();

    static {
        for (Field field : Opcodes.class.getDeclaredFields()) {
            try {
                if (field.getName().startsWith("H_"))
                    tags.put(field.getInt(null), field.getName());
            } catch (IllegalAccessException e) {
                e.printStackTrace();
            }
        }

        // <editor-fold defaultstate="collapsed" desc="This is sad.">
        opcodes.put(AALOAD, "AALOAD");
		opcodes.put(AASTORE, "AASTORE");
		opcodes.put(ACONST_NULL, "ACONST_NULL");
		opcodes.put(ALOAD, "ALOAD");
		opcodes.put(ANEWARRAY, "ANEWARRAY");
		opcodes.put(ARETURN, "ARETURN");
		opcodes.put(ARRAYLENGTH, "ARRAYLENGTH");
		opcodes.put(ASTORE, "ASTORE");
		opcodes.put(ATHROW, "ATHROW");
		opcodes.put(BALOAD, "BALOAD");
		opcodes.put(BASTORE, "BASTORE");
		opcodes.put(BIPUSH, "BIPUSH");
		opcodes.put(CALOAD, "CALOAD");
		opcodes.put(CASTORE, "CASTORE");
		opcodes.put(CHECKCAST, "CHECKCAST");
		opcodes.put(D2F, "D2F");
		opcodes.put(D2I, "D2I");
		opcodes.put(D2L, "D2L");
		opcodes.put(DADD, "DADD");
		opcodes.put(DALOAD, "DALOAD");
		opcodes.put(DASTORE, "DASTORE");
		opcodes.put(DCMPG, "DCMPG");
		opcodes.put(DCMPL, "DCMPL");
		opcodes.put(DCONST_0, "DCONST_0");
		opcodes.put(DCONST_1, "DCONST_1");
		opcodes.put(DDIV, "DDIV");
		opcodes.put(DLOAD, "DLOAD");
		opcodes.put(DMUL, "DMUL");
		opcodes.put(DNEG, "DNEG");
		opcodes.put(DREM, "DREM");
		opcodes.put(DRETURN, "DRETURN");
		opcodes.put(DSTORE, "DSTORE");
		opcodes.put(DSUB, "DSUB");
		opcodes.put(DUP, "DUP");
		opcodes.put(DUP2, "DUP2");
		opcodes.put(DUP2_X1, "DUP2_X1");
		opcodes.put(DUP2_X2, "DUP2_X2");
		opcodes.put(DUP_X1, "DUP_X1");
		opcodes.put(DUP_X2, "DUP_X2");
		opcodes.put(F2D, "F2D");
		opcodes.put(F2I, "F2I");
		opcodes.put(F2L, "F2L");
		opcodes.put(F_NEW, "F_NEW");
		opcodes.put(FADD, "FADD");
		opcodes.put(FALOAD, "FALOAD");
		opcodes.put(FASTORE, "FASTORE");
		opcodes.put(FCMPG, "FCMPG");
		opcodes.put(FCMPL, "FCMPL");
		opcodes.put(FCONST_0, "FCONST_0");
		opcodes.put(FCONST_1, "FCONST_1");
		opcodes.put(FCONST_2, "FCONST_2");
		opcodes.put(FDIV, "FDIV");
		opcodes.put(FLOAD, "FLOAD");
		opcodes.put(FMUL, "FMUL");
		opcodes.put(FNEG, "FNEG");
		opcodes.put(FREM, "FREM");
		opcodes.put(FRETURN, "FRETURN");
		opcodes.put(FSTORE, "FSTORE");
		opcodes.put(FSUB, "FSUB");
		opcodes.put(GETFIELD, "GETFIELD");
		opcodes.put(GETSTATIC, "GETSTATIC");
		opcodes.put(GOTO, "GOTO");
		opcodes.put(I2B, "I2B");
		opcodes.put(I2C, "I2C");
		opcodes.put(I2D, "I2D");
		opcodes.put(I2F, "I2F");
		opcodes.put(I2L, "I2L");
		opcodes.put(I2S, "I2S");
		opcodes.put(IADD, "IADD");
		opcodes.put(IALOAD, "IALOAD");
		opcodes.put(IAND, "IAND");
		opcodes.put(IASTORE, "IASTORE");
		opcodes.put(ICONST_0, "ICONST_0");
		opcodes.put(ICONST_1, "ICONST_1");
		opcodes.put(ICONST_2, "ICONST_2");
		opcodes.put(ICONST_3, "ICONST_3");
		opcodes.put(ICONST_4, "ICONST_4");
		opcodes.put(ICONST_5, "ICONST_5");
		opcodes.put(ICONST_M1, "ICONST_M1");
		opcodes.put(IDIV, "IDIV");
		opcodes.put(IF_ACMPEQ, "IF_ACMPEQ");
		opcodes.put(IF_ACMPNE, "IF_ACMPNE");
		opcodes.put(IF_ICMPEQ, "IF_ICMPEQ");
		opcodes.put(IF_ICMPGE, "IF_ICMPGE");
		opcodes.put(IF_ICMPGT, "IF_ICMPGT");
		opcodes.put(IF_ICMPLE, "IF_ICMPLE");
		opcodes.put(IF_ICMPLT, "IF_ICMPLT");
		opcodes.put(IF_ICMPNE, "IF_ICMPNE");
		opcodes.put(IFEQ, "IFEQ");
		opcodes.put(IFGE, "IFGE");
		opcodes.put(IFGT, "IFGT");
		opcodes.put(IFLE, "IFLE");
		opcodes.put(IFLT, "IFLT");
		opcodes.put(IFNE, "IFNE");
		opcodes.put(IFNONNULL, "IFNONNULL");
		opcodes.put(IFNULL, "IFNULL");
		opcodes.put(IINC, "IINC");
		opcodes.put(ILOAD, "ILOAD");
		opcodes.put(IMUL, "IMUL");
		opcodes.put(INEG, "INEG");
		opcodes.put(INSTANCEOF, "INSTANCEOF");
		opcodes.put(INVOKEDYNAMIC, "INVOKEDYNAMIC");
		opcodes.put(INVOKEINTERFACE, "INVOKEINTERFACE");
		opcodes.put(INVOKESPECIAL, "INVOKESPECIAL");
		opcodes.put(INVOKESTATIC, "INVOKESTATIC");
		opcodes.put(INVOKEVIRTUAL, "INVOKEVIRTUAL");
		opcodes.put(IOR, "IOR");
		opcodes.put(IREM, "IREM");
		opcodes.put(IRETURN, "IRETURN");
		opcodes.put(ISHL, "ISHL");
		opcodes.put(ISHR, "ISHR");
		opcodes.put(ISTORE, "ISTORE");
		opcodes.put(ISUB, "ISUB");
		opcodes.put(IUSHR, "IUSHR");
		opcodes.put(IXOR, "IXOR");
		opcodes.put(JSR, "JSR");
		opcodes.put(L2D, "L2D");
		opcodes.put(L2F, "L2F");
		opcodes.put(L2I, "L2I");
		opcodes.put(LADD, "LADD");
		opcodes.put(LALOAD, "LALOAD");
		opcodes.put(LAND, "LAND");
		opcodes.put(LASTORE, "LASTORE");
		opcodes.put(LCMP, "LCMP");
		opcodes.put(LCONST_0, "LCONST_0");
		opcodes.put(LCONST_1, "LCONST_1");
		opcodes.put(LDC, "LDC");
		opcodes.put(LDIV, "LDIV");
		opcodes.put(LLOAD, "LLOAD");
		opcodes.put(LMUL, "LMUL");
		opcodes.put(LNEG, "LNEG");
		opcodes.put(LOOKUPSWITCH, "LOOKUPSWITCH");
		opcodes.put(LOR, "LOR");
		opcodes.put(LREM, "LREM");
		opcodes.put(LRETURN, "LRETURN");
		opcodes.put(LSHL, "LSHL");
		opcodes.put(LSHR, "LSHR");
		opcodes.put(LSTORE, "LSTORE");
		opcodes.put(LSUB, "LSUB");
		opcodes.put(LUSHR, "LUSHR");
		opcodes.put(LXOR, "LXOR");
		opcodes.put(MONITORENTER, "MONITORENTER");
		opcodes.put(MONITOREXIT, "MONITOREXIT");
		opcodes.put(MULTIANEWARRAY, "MULTIANEWARRAY");
		opcodes.put(NEW, "NEW");
		opcodes.put(NEWARRAY, "NEWARRAY");
		opcodes.put(NOP, "NOP");
		opcodes.put(POP, "POP");
		opcodes.put(POP2, "POP2");
		opcodes.put(PUTFIELD, "PUTFIELD");
		opcodes.put(PUTSTATIC, "PUTSTATIC");
		opcodes.put(RET, "RET");
		opcodes.put(RETURN, "RETURN");
		opcodes.put(SALOAD, "SALOAD");
		opcodes.put(SASTORE, "SASTORE");
		opcodes.put(SIPUSH, "SIPUSH");
		opcodes.put(SWAP, "SWAP");
		opcodes.put(TABLESWITCH, "TABLESWITCH");
		// </editor-fold>
		// <editor-fold defaultstate="collapsed" desc="This is also sad.">
        types.put(NOP, AbstractInsnNode.INSN);
		types.put(ACONST_NULL, AbstractInsnNode.INSN);
		types.put(ICONST_M1, AbstractInsnNode.INSN);
		types.put(ICONST_0, AbstractInsnNode.INSN);
		types.put(ICONST_1, AbstractInsnNode.INSN);
		types.put(ICONST_2, AbstractInsnNode.INSN);
		types.put(ICONST_3, AbstractInsnNode.INSN);
		types.put(ICONST_4, AbstractInsnNode.INSN);
		types.put(ICONST_5, AbstractInsnNode.INSN);
		types.put(LCONST_0, AbstractInsnNode.INSN);
		types.put(LCONST_1, AbstractInsnNode.INSN);
		types.put(FCONST_0, AbstractInsnNode.INSN);
		types.put(FCONST_1, AbstractInsnNode.INSN);
		types.put(FCONST_2, AbstractInsnNode.INSN);
		types.put(DCONST_0, AbstractInsnNode.INSN);
		types.put(DCONST_1, AbstractInsnNode.INSN);
		types.put(IALOAD, AbstractInsnNode.INSN);
		types.put(LALOAD, AbstractInsnNode.INSN);
		types.put(FALOAD, AbstractInsnNode.INSN);
		types.put(DALOAD, AbstractInsnNode.INSN);
		types.put(AALOAD, AbstractInsnNode.INSN);
		types.put(BALOAD, AbstractInsnNode.INSN);
		types.put(CALOAD, AbstractInsnNode.INSN);
		types.put(SALOAD, AbstractInsnNode.INSN);
		types.put(IASTORE, AbstractInsnNode.INSN);
		types.put(LASTORE, AbstractInsnNode.INSN);
		types.put(FASTORE, AbstractInsnNode.INSN);
		types.put(DASTORE, AbstractInsnNode.INSN);
		types.put(AASTORE, AbstractInsnNode.INSN);
		types.put(BASTORE, AbstractInsnNode.INSN);
		types.put(CASTORE, AbstractInsnNode.INSN);
		types.put(SASTORE, AbstractInsnNode.INSN);
		types.put(POP, AbstractInsnNode.INSN);
		types.put(POP2, AbstractInsnNode.INSN);
		types.put(DUP, AbstractInsnNode.INSN);
		types.put(DUP_X1, AbstractInsnNode.INSN);
		types.put(DUP_X2, AbstractInsnNode.INSN);
		types.put(DUP2, AbstractInsnNode.INSN);
		types.put(DUP2_X1, AbstractInsnNode.INSN);
		types.put(DUP2_X2, AbstractInsnNode.INSN);
		types.put(SWAP, AbstractInsnNode.INSN);
		types.put(IADD, AbstractInsnNode.INSN);
		types.put(LADD, AbstractInsnNode.INSN);
		types.put(FADD, AbstractInsnNode.INSN);
		types.put(DADD, AbstractInsnNode.INSN);
		types.put(ISUB, AbstractInsnNode.INSN);
		types.put(LSUB, AbstractInsnNode.INSN);
		types.put(FSUB, AbstractInsnNode.INSN);
		types.put(DSUB, AbstractInsnNode.INSN);
		types.put(IMUL, AbstractInsnNode.INSN);
		types.put(LMUL, AbstractInsnNode.INSN);
		types.put(FMUL, AbstractInsnNode.INSN);
		types.put(DMUL, AbstractInsnNode.INSN);
		types.put(IDIV, AbstractInsnNode.INSN);
		types.put(LDIV, AbstractInsnNode.INSN);
		types.put(FDIV, AbstractInsnNode.INSN);
		types.put(DDIV, AbstractInsnNode.INSN);
		types.put(IREM, AbstractInsnNode.INSN);
		types.put(LREM, AbstractInsnNode.INSN);
		types.put(FREM, AbstractInsnNode.INSN);
		types.put(DREM, AbstractInsnNode.INSN);
		types.put(INEG, AbstractInsnNode.INSN);
		types.put(LNEG, AbstractInsnNode.INSN);
		types.put(FNEG, AbstractInsnNode.INSN);
		types.put(DNEG, AbstractInsnNode.INSN);
		types.put(ISHL, AbstractInsnNode.INSN);
		types.put(LSHL, AbstractInsnNode.INSN);
		types.put(ISHR, AbstractInsnNode.INSN);
		types.put(LSHR, AbstractInsnNode.INSN);
		types.put(IUSHR, AbstractInsnNode.INSN);
		types.put(LUSHR, AbstractInsnNode.INSN);
		types.put(IAND, AbstractInsnNode.INSN);
		types.put(LAND, AbstractInsnNode.INSN);
		types.put(IOR, AbstractInsnNode.INSN);
		types.put(LOR, AbstractInsnNode.INSN);
		types.put(IXOR, AbstractInsnNode.INSN);
		types.put(LXOR, AbstractInsnNode.INSN);
		types.put(I2L, AbstractInsnNode.INSN);
		types.put(I2F, AbstractInsnNode.INSN);
		types.put(I2D, AbstractInsnNode.INSN);
		types.put(L2I, AbstractInsnNode.INSN);
		types.put(L2F, AbstractInsnNode.INSN);
		types.put(L2D, AbstractInsnNode.INSN);
		types.put(F2I, AbstractInsnNode.INSN);
		types.put(F2L, AbstractInsnNode.INSN);
		types.put(F2D, AbstractInsnNode.INSN);
		types.put(D2I, AbstractInsnNode.INSN);
		types.put(D2L, AbstractInsnNode.INSN);
		types.put(D2F, AbstractInsnNode.INSN);
		types.put(I2B, AbstractInsnNode.INSN);
		types.put(I2C, AbstractInsnNode.INSN);
		types.put(I2S, AbstractInsnNode.INSN);
		types.put(LCMP, AbstractInsnNode.INSN);
		types.put(FCMPL, AbstractInsnNode.INSN);
		types.put(FCMPG, AbstractInsnNode.INSN);
		types.put(DCMPL, AbstractInsnNode.INSN);
		types.put(DCMPG, AbstractInsnNode.INSN);
		types.put(IRETURN, AbstractInsnNode.INSN);
		types.put(LRETURN, AbstractInsnNode.INSN);
		types.put(FRETURN, AbstractInsnNode.INSN);
		types.put(DRETURN, AbstractInsnNode.INSN);
		types.put(ARETURN, AbstractInsnNode.INSN);
		types.put(RETURN, AbstractInsnNode.INSN);
		types.put(ARRAYLENGTH, AbstractInsnNode.INSN);
		types.put(ATHROW, AbstractInsnNode.INSN);
		types.put(MONITORENTER, AbstractInsnNode.INSN);
		types.put(MONITOREXIT, AbstractInsnNode.INSN);
		types.put(BIPUSH, AbstractInsnNode.INT_INSN);
		types.put(SIPUSH, AbstractInsnNode.INT_INSN);
		types.put(NEWARRAY, AbstractInsnNode.INT_INSN);
		types.put(ILOAD, AbstractInsnNode.VAR_INSN);
		types.put(LLOAD, AbstractInsnNode.VAR_INSN);
		types.put(FLOAD, AbstractInsnNode.VAR_INSN);
		types.put(DLOAD, AbstractInsnNode.VAR_INSN);
		types.put(ALOAD, AbstractInsnNode.VAR_INSN);
		types.put(ISTORE, AbstractInsnNode.VAR_INSN);
		types.put(LSTORE, AbstractInsnNode.VAR_INSN);
		types.put(FSTORE, AbstractInsnNode.VAR_INSN);
		types.put(DSTORE, AbstractInsnNode.VAR_INSN);
		types.put(ASTORE, AbstractInsnNode.VAR_INSN);
		types.put(RET, AbstractInsnNode.VAR_INSN);
		types.put(NEW, AbstractInsnNode.TYPE_INSN);
		types.put(ANEWARRAY, AbstractInsnNode.TYPE_INSN);
		types.put(CHECKCAST, AbstractInsnNode.TYPE_INSN);
		types.put(INSTANCEOF, AbstractInsnNode.TYPE_INSN);
		types.put(GETSTATIC, AbstractInsnNode.FIELD_INSN);
		types.put(GETFIELD, AbstractInsnNode.FIELD_INSN);
		types.put(PUTSTATIC, AbstractInsnNode.FIELD_INSN);
		types.put(PUTFIELD, AbstractInsnNode.FIELD_INSN);
		types.put(INVOKEVIRTUAL, AbstractInsnNode.METHOD_INSN);
		types.put(INVOKESPECIAL, AbstractInsnNode.METHOD_INSN);
		types.put(INVOKESTATIC, AbstractInsnNode.METHOD_INSN);
		types.put(INVOKEINTERFACE, AbstractInsnNode.METHOD_INSN);
		types.put(INVOKEDYNAMIC, AbstractInsnNode.INVOKE_DYNAMIC_INSN);
		types.put(IFEQ, AbstractInsnNode.JUMP_INSN);
		types.put(IFNE, AbstractInsnNode.JUMP_INSN);
		types.put(IFLT, AbstractInsnNode.JUMP_INSN);
		types.put(IFGE, AbstractInsnNode.JUMP_INSN);
		types.put(IFGT, AbstractInsnNode.JUMP_INSN);
		types.put(IFLE, AbstractInsnNode.JUMP_INSN);
		types.put(IF_ICMPEQ, AbstractInsnNode.JUMP_INSN);
		types.put(IF_ICMPNE, AbstractInsnNode.JUMP_INSN);
		types.put(IF_ICMPLT, AbstractInsnNode.JUMP_INSN);
		types.put(IF_ICMPGE, AbstractInsnNode.JUMP_INSN);
		types.put(IF_ICMPGT, AbstractInsnNode.JUMP_INSN);
		types.put(IF_ICMPLE, AbstractInsnNode.JUMP_INSN);
		types.put(IF_ACMPEQ, AbstractInsnNode.JUMP_INSN);
		types.put(IF_ACMPNE, AbstractInsnNode.JUMP_INSN);
		types.put(GOTO, AbstractInsnNode.JUMP_INSN);
		types.put(JSR, AbstractInsnNode.JUMP_INSN);
		types.put(IFNULL, AbstractInsnNode.JUMP_INSN);
		types.put(IFNONNULL, AbstractInsnNode.JUMP_INSN);
		types.put(LDC, AbstractInsnNode.LDC_INSN);
		types.put(IINC, AbstractInsnNode.IINC_INSN);
		types.put(TABLESWITCH, AbstractInsnNode.TABLESWITCH_INSN);
		types.put(LOOKUPSWITCH, AbstractInsnNode.LOOKUPSWITCH_INSN);
		types.put(MULTIANEWARRAY, AbstractInsnNode.MULTIANEWARRAY_INSN);
		// </editor-fold>
    }

	/**
	 * Creates a copy of an InsnList.
	 * @param insnList The InsnList to copy.
	 * @return A copy of the InsnList.
	 */
	public static InsnList copy(InsnList insnList) {
		MethodNode mv = new MethodNode();
		insnList.accept(mv);
		return mv.instructions;
	}

	/**
	 * Finds the method with the given signature.
	 * @param methodSignature The method signature, in the format "your/Class's.method(Land/the/Params;)Lreturn/Type;"
	 * @return The method node, or null if it could not be found.
	 */
	public static MethodNode findMethod(String methodSignature) {
		MethodNode method = null;
		String className = methodSignature.substring(0, methodSignature.lastIndexOf('.'));
		String methodName = methodSignature.substring(methodSignature.lastIndexOf('.') + 1, methodSignature.indexOf('('));
		String methodDesc = methodSignature.substring(methodSignature.indexOf('('));
		//in Main.classes, find the class that contains the method
        for (ClassNode node : classes) {
            if (node.name.equals(className)) {
                for (int j = 0; j < node.methods.size(); j++) {
                    MethodNode methodNode = node.methods.get(j);
                    if (methodNode.name.equals(methodName) && methodNode.desc.equals(methodDesc)) {
                        method = methodNode;
                        break;
                    }
                }
                break;
            }
        }
		return method;
	}
}
