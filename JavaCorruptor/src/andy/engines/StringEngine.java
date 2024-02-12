package andy.engines;

import org.objectweb.asm.tree.AbstractInsnNode;
import org.objectweb.asm.tree.InsnList;
import org.objectweb.asm.tree.LdcInsnNode;

import static andy.Main.valueRandom;

public class StringEngine extends AbstractEngine {
    private final int mode;
    private final String charset;
    private final float percentage;

    public StringEngine(String[] args) {
        mode = Integer.parseInt(args[6]);
        charset = args[7];
        percentage = Float.parseFloat(args[8]);
    }

    @Override
    protected InsnList doCorrupt(AbstractInsnNode insn, int insnIndex) { // TODO: so much
        InsnList list = new InsnList();

        if (!(insn instanceof LdcInsnNode)) {
            list.add(insn);
            return list;
        }

        LdcInsnNode ldcInsn = (LdcInsnNode)insn;
        if (!(ldcInsn.cst instanceof String)) {
            list.add(insn);
            return list;
        }

        if (mode == 0) { // Nightmare mode: Replace random letters in the string with random characters from the charset
            String s = (String)ldcInsn.cst;
            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < s.length(); i++)
                if (valueRandom.nextFloat() < percentage)
                    sb.append(charset.charAt(valueRandom.nextInt(charset.length())));
                else
                    sb.append(s.charAt(i));

            ldcInsn.cst = sb.toString();
            list.add(ldcInsn);
            return list;
        }
        if (mode == 1) { // Swap mode: Swap random letters in the string with each other
            String s = (String)ldcInsn.cst;
            StringBuilder sb = new StringBuilder(s);

            for (int i = 0; i < s.length(); i++)
                if (valueRandom.nextFloat() < percentage) {
                    int j = valueRandom.nextInt(s.length());
                    char c = sb.charAt(i);
                    sb.setCharAt(i, sb.charAt(j));
                    sb.setCharAt(j, c);
                }

            ldcInsn.cst = sb.toString();
            list.add(ldcInsn);
            return list;
        }

        return list;
    }
}
