package andy;

import org.objectweb.asm.ClassReader;
import org.objectweb.asm.tree.*;
import org.apache.commons.io.IOUtils;

import java.io.*;
import java.util.*;
import java.util.concurrent.atomic.AtomicInteger;
import java.util.jar.*;

public class Main {
    public static void main(String[] args) throws Exception {
        JarFile jarFile = new JarFile("C:\\Users\\me\\AppData\\Roaming\\.minecraft\\versions\\1.20.2\\1.20.2.jar");
        Enumeration<JarEntry> entries = jarFile.entries();

        AtomicInteger highestLabelCount = new AtomicInteger(0);
        while (entries.hasMoreElements()) {
            JarEntry jarEntry = entries.nextElement();

            InputStream entryInputStream = jarFile.getInputStream(jarEntry);
            byte[] entryBytes = IOUtils.toByteArray(entryInputStream);
            entryInputStream.close();

            if (jarEntry.getName().endsWith(".class")) {
                ClassReader classReader = new ClassReader(entryBytes); //TODO: unnecessary, use ClassReader(InputStream)
                ClassNode classNode = new ClassNode();
                classReader.accept(classNode, 0);
                classNode.methods.forEach(methodNode -> {
                    AtomicInteger labelCount = new AtomicInteger(0);
                    if (methodNode.instructions != null) {
                        methodNode.instructions.forEach(insnNode -> {
                            if (insnNode instanceof LabelNode) {
                                labelCount.getAndIncrement();
                            }
                        });
                    }
                    if (labelCount.get() > highestLabelCount.get()) {
                        highestLabelCount.set(labelCount.get());
                    }
                });

                AsmUtilities.classes.add(classNode);
            }
        }

        System.out.println("Highest label count: " + highestLabelCount.get());
    }
}
