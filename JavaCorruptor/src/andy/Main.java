package andy;
import org.objectweb.asm.ClassReader;
import org.objectweb.asm.ClassWriter;
import org.objectweb.asm.Opcodes;
import org.objectweb.asm.tree.*;
import org.apache.commons.io.IOUtils;
import java.io.*;
import java.util.*;
import java.util.jar.*;

public class Main {
    private static Random random1;
    private static Random random2;
    private static int mode;
    private static double intensity;
    private static final List<Integer> intMath = new ArrayList<>();
    private static final List<Integer> longMath = new ArrayList<>();
    private static final List<Integer> floatMath = new ArrayList<>();
    private static final List<Integer> doubleMath = new ArrayList<>();

    public static void main(String[] args) throws Exception {
        intMath.add(Opcodes.IADD);
        intMath.add(Opcodes.ISUB);
        intMath.add(Opcodes.IMUL);
        intMath.add(Opcodes.IDIV);
        longMath.add(Opcodes.LADD);
        longMath.add(Opcodes.LSUB);
        longMath.add(Opcodes.LMUL);
        longMath.add(Opcodes.LDIV);
        floatMath.add(Opcodes.FADD);
        floatMath.add(Opcodes.FSUB);
        floatMath.add(Opcodes.FMUL);
        floatMath.add(Opcodes.FDIV);
        doubleMath.add(Opcodes.DADD);
        doubleMath.add(Opcodes.DSUB);
        doubleMath.add(Opcodes.DMUL);
        doubleMath.add(Opcodes.DDIV);

        String inPath = args[0];
        String outPath = args[1];
        random1 = new Random(Long.parseLong(args[2]));
        random2 = new Random(Long.parseLong(args[3]));
        mode = Integer.parseInt(args[4]);
        intensity = Double.parseDouble(args[5]);

        Map<String, byte[]> modifiedClasses = new HashMap<>();
        Map<String, byte[]> resources = new HashMap<>();

        JarFile jarFile = new JarFile(inPath);
        Enumeration<JarEntry> entries = jarFile.entries();
        while (entries.hasMoreElements()) {
            JarEntry jarEntry = entries.nextElement();

            InputStream entryInputStream = jarFile.getInputStream(jarEntry);
            byte[] entryBytes = IOUtils.toByteArray(entryInputStream);
            entryInputStream.close();

            if (jarEntry.getName().endsWith(".class")) {
                ClassReader classReader = new ClassReader(entryBytes);
                ClassNode classNode = new ClassNode();
                classReader.accept(classNode, 0);

                corrupt(classNode, args);

                ClassWriter classWriter = new ClassWriter(ClassWriter.COMPUTE_MAXS);
                classNode.accept(classWriter);
                modifiedClasses.put(jarEntry.getName(), classWriter.toByteArray());
            } else {
                resources.put(jarEntry.getName(), entryBytes);
            }
        }


        JarOutputStream corruptedJarOutputStream = new JarOutputStream(new FileOutputStream(outPath));
            for (Map.Entry<String, byte[]> entry : modifiedClasses.entrySet()) {
                JarEntry modifiedJarEntry = new JarEntry(entry.getKey());
                corruptedJarOutputStream.putNextEntry(modifiedJarEntry);
                corruptedJarOutputStream.write(entry.getValue());
                corruptedJarOutputStream.closeEntry();
            }

            for (Map.Entry<String, byte[]> entry : resources.entrySet()) {
                JarEntry resourceJarEntry = new JarEntry(entry.getKey());
                corruptedJarOutputStream.putNextEntry(resourceJarEntry);
                corruptedJarOutputStream.write(entry.getValue());
                corruptedJarOutputStream.closeEntry();
            }
        corruptedJarOutputStream.close();
    }

    private static AbstractInsnNode vector(AbstractInsnNode insn, int limiter, int value) {
        // TODO: so much
        if (insn.getOpcode() == limiter)
            return new InsnNode(value);
        else
            return insn;
    }
    private static InsnList arithmetic(AbstractInsnNode insn, float max, float min, List<Integer> limiters, List<Integer> operations, int types) {
        InsnList insnList = new InsnList();
        insnList.add(insn);

        int opcode = insn.getOpcode();
        if (opcode < 0x60 || opcode > 0x6f)
            return insnList;
        if (!limiters.contains(opcode - 0x60 - ((opcode - 0x60) % 4))) //from 0x60-0x6f, each grouping of 4 is the I,L,F, and D instructions of each operation.
            return insnList;

        float f = random2.nextFloat();
        float value = f > 0 ? f * max : f * -min;
        int operation = (operations.get(random2.nextInt(operations.size())));

        //TODO: this could probably be a loop or something, but it's 3am and i'm too tired to think
        switch ((opcode - 0x60) % 4) {
            case 0:
                if ((types & 1) == 1) {
                    insnList.add(new LdcInsnNode((int) value));
                    insnList.add(new InsnNode(operation + 0x60));
                }
                return insnList;
            case 1:
                if ((types & 2) == 2) {
                    insnList.add(new LdcInsnNode((long) value));
                    insnList.add(new InsnNode(operation + 0x61));
                }
                return insnList;
            case 2:
                if ((types & 4) == 4) {
                    insnList.add(new LdcInsnNode(value));
                    insnList.add(new InsnNode(operation + 0x62));
                }
                return insnList;
            case 3:
                if ((types & 8) == 8) {
                    insnList.add(new LdcInsnNode((double)value));
                    insnList.add(new InsnNode(operation + 0x63));
                }
                return insnList;
        }

        return insnList;
    }
    private static InsnList function(MethodInsnNode insn, List<String> limiters, List<String> values) {
        InsnList list = new InsnList();
        if (limiters.contains(insn.name)) {
            String newMethod = values.get(random2.nextInt(values.size()));
            if (newMethod.equals("POP,random()")) {
                list.add(new InsnNode(Opcodes.POP));
                list.add(new MethodInsnNode(Opcodes.INVOKESTATIC, "java/lang/Math", "random", "()D", false));
            } else {
                insn.name = newMethod;
                list.add(insn);
            }
        } else {
            list.add(insn);
        }
        return list;
    }

    private static void corrupt(ClassNode clazz, String[] args) {
        clazz.methods.forEach(methodNode -> {
            final InsnList insnList = new InsnList();

            methodNode.instructions.forEach(insnNode -> {
                if (random1.nextDouble() > intensity) {
                    insnList.add(insnNode);
                    return;
                }
                switch (mode) {
                    case 0: {
                        insnList.add(vector(insnNode, Integer.parseInt(args[6]), Integer.parseInt(args[7])));
                        break;
                    }
                    case 1: {
                        List<Integer> limiters = new ArrayList<>();
                        List<Integer> operations = new ArrayList<>();
                        int i = 8;
                        for (; !args[i].equals(":"); i++)
                            limiters.add(Integer.parseInt(args[i]));
                        i++;
                        for (; !args[i].equals(":"); i++)
                            operations.add(Integer.parseInt(args[i]));
                        i++;

                        insnList.add(arithmetic(insnNode, Float.parseFloat(args[6]), Float.parseFloat(args[7]), limiters, operations, Integer.parseInt(args[i])));
                        break;
                    }
                    case 2: {
                        if (insnNode.getOpcode() != Opcodes.INVOKESTATIC) {
                            insnList.add(insnNode);
                            break;
                        }
                        MethodInsnNode methodInsnNode = (MethodInsnNode) insnNode;
                        if (!methodInsnNode.owner.equals("java/lang/Math") || !methodInsnNode.desc.equals("(D)D")) {
                            insnList.add(insnNode);
                            break;
                        }

                        List<String> limiters = new ArrayList<>();
                        List<String> values = new ArrayList<>();
                        int i = 6;
                        for (; !args[i].equals(":"); i++)
                            limiters.add(args[i]);
                        i++;
                        for (; !args[i].equals(":"); i++)
                            values.add(args[i]);

                        insnList.add(function(methodInsnNode, limiters, values));

                        break;
                    }
                    default:
                        insnList.add(insnNode);
                        break;
                }
            });

            methodNode.instructions = insnList;
        });
    }
}
