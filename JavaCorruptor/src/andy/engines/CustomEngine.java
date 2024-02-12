package andy.engines;

import andy.AsmParser;
import org.objectweb.asm.tree.AbstractInsnNode;
import org.objectweb.asm.tree.InsnList;

import java.nio.file.Files;
import java.nio.file.Paths;
import java.util.regex.Matcher;
import java.util.regex.Pattern;

/*
 * A custom engine has two sections: [FIND] and [REPLACE]
 * [FIND] is a list of instructions to find in the method.
 * [REPLACE] is a list of instructions to replace the found instructions with.
 * You can use regular expressions by surrounding them with <angle brackets> in the [FIND] section. If you have capture groups, you can reference these groups the same way in the [REPLACE] section.
 * Block comments and line comments are allowed anywhere and are removed before parsing.
 *
 * Example:
 * [FIND]
 * INVOKESTATIC java/lang/Math.random()D
 * INVOKESTATIC java/lang/Math.random()D
 *
 * [REPLACE]
 * LDC 0.0
 * LDC 0.0
 *
 * This will replace two calls to Math.random() with two LDC 0.0 instructions.
 *
 * Example:
 * [FIND]
 * FCONST_<(.*)>
 *
 * [REPLACE]
 * LDC <$1>f
 *
 * This will replace any FCONST instruction with an LDC instruction with the same value.
 *
 *
 * Quirks:
 * - Labels are not supported properly in the [FIND] section, including labels on line instructions. Labels will all be called "LABEL_?" and will not be able to be referenced in the [REPLACE] section.
 * - Variable names are not supported, use the index of the variable instead.
 */

public class CustomEngine extends AbstractEngine {
    private final String[] findInstructions, replaceInstructions;
    private final String findList, replaceList;

    public CustomEngine(String[] args) throws Exception {
        String code = new String(Files.readAllBytes(Paths.get(args[6])));

        code = code.replaceAll("(?s)/\\*.*?\\*/", ""); // removes block comments
        code = code.replaceAll("//.*", ""); // removes line comments

        int index = code.indexOf("[FIND]");
        String find = code.substring(index + 6, code.indexOf("[REPLACE]")).trim();
        String replace = code.substring(code.indexOf("[REPLACE]") + 9).trim();
        findInstructions = find.split(System.lineSeparator());
        replaceInstructions = replace.split(System.lineSeparator());
        replaceList = replace;

        for (int i = 0; i < findInstructions.length; i++) {
            String parsing = findInstructions[i];
            parsing = "\\Q" + parsing.replace("<", "\\E").replace(">", "\\Q") + "\\E";
            findInstructions[i] = parsing.replace("\\Q\\E", "");
        }
        findList = String.join(System.lineSeparator(), findInstructions);
    }

    @Override
    protected InsnList doCorrupt(AbstractInsnNode insn, int insnIndex) {
        InsnList list = new InsnList();
        AbstractInsnNode currentInsn = insn;
        StringBuilder insns = new StringBuilder();

        for (int i = 0; i < findInstructions.length; i++) {
            String insnString = AsmParser.insnToString(currentInsn);

            if (!Pattern.matches(findInstructions[i], insnString))
                break;

            insns.append(insnString).append(System.lineSeparator());

            if (i == findInstructions.length - 1) {
                Matcher m = Pattern.compile(findList).matcher(insns);
                StringBuilder newInsns = new StringBuilder();

                while (m.find()) {
                    String replace = replaceList;
                    for (int j = 0; j < m.groupCount(); j++) {
                        String group = m.group(j + 1);
                        replace = replace.replace("<$" + j + ">", group);
                    }
                    newInsns.append(replace).append(System.lineSeparator());
                }

                String[] newInsnStrings = newInsns.toString().split(System.lineSeparator());
                for (String newInsnString : newInsnStrings) {
                    AbstractInsnNode newInsn = AsmParser.parseInsn(newInsnString);
                    list.add(newInsn);
                }

                return list;
            }
            currentInsn = currentInsn.getNext();
        }

        if (list.size() == 0)
            list.add(insn);

        return list;
    }
}
