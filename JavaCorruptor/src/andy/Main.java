package andy;

import andy.engines.*;
import com.google.gson.GsonBuilder;
import org.objectweb.asm.ClassReader;
import org.objectweb.asm.ClassWriter;
import org.objectweb.asm.tree.*;
import org.apache.commons.io.IOUtils;

import java.io.*;
import java.nio.file.Files;
import java.nio.file.Paths;
import java.util.*;
import java.util.concurrent.atomic.AtomicInteger;
import java.util.jar.*;

public class Main {
    public static double intensity;
    private static AbstractEngine engine;
    public static Random instructionRandom;
    public static Random valueRandom;
    public static BlastLayer blastLayer = new BlastLayer();

    private static final List<Class<? extends AbstractEngine>> engines = new ArrayList<>(7);
    static {
        engines.add(VectorEngine.class);
        engines.add(ArithmeticEngine.class);
        engines.add(FunctionEngine.class);
        engines.add(CustomEngine.class);
        engines.add(BlastLayerEngine.class);
        engines.add(StringEngine.class);
        engines.add(RoundingEngine.class);
    }


    public static void main(String[] args) throws Exception {
        String inPath = args[0];
        String outPath = args[1];
        instructionRandom = new Random(Long.parseLong(args[2]));
        valueRandom = new Random(Long.parseLong(args[3]));
        int mode = Integer.parseInt(args[4]);
        intensity = Double.parseDouble(args[5]);


        Map<String, byte[]> modifiedClasses = new HashMap<>();
        Map<String, byte[]> resources = new HashMap<>();

        JarFile jarFile = new JarFile(inPath);
        Enumeration<JarEntry> entries = jarFile.entries();

        long nanoTime = System.nanoTime();
        if (mode == 4) {
            while (entries.hasMoreElements()) {
                JarEntry jarEntry = entries.nextElement();

                InputStream entryInputStream = jarFile.getInputStream(jarEntry);
                byte[] entryBytes = IOUtils.toByteArray(entryInputStream);
                entryInputStream.close();

                if (jarEntry.getName().endsWith(".class")) {
                    ClassReader classReader = new ClassReader(entryBytes); //TODO: unnecessary, use ClassReader(InputStream)
                    ClassNode classNode = new ClassNode();
                    classReader.accept(classNode, 0);

                    AsmUtilities.classes.add(classNode);
                }
            }

            entries = jarFile.entries();
        }

        engine = engines.get(mode).getConstructor(String[].class).newInstance((Object) args);

        while (entries.hasMoreElements()) {
            JarEntry jarEntry = entries.nextElement();

            InputStream entryInputStream = jarFile.getInputStream(jarEntry);
            byte[] entryBytes = IOUtils.toByteArray(entryInputStream);
            entryInputStream.close();

            if (jarEntry.getName().endsWith(".class")) {
                ClassReader classReader = new ClassReader(entryBytes); //TODO: unnecessary, use ClassReader(InputStream)
                ClassNode classNode = new ClassNode();
                if (mode != 4)
                    AsmUtilities.classes.add(classNode);

                classReader.accept(classNode, 0);

                engine.corrupt(classNode);

                ClassWriter classWriter = new ClassWriter(ClassWriter.COMPUTE_MAXS);
                classNode.accept(classWriter);
                modifiedClasses.put(jarEntry.getName(), classWriter.toByteArray());
            } else {
                resources.put(jarEntry.getName(), entryBytes);
            }
        }


        JarOutputStream corruptedJarOutputStream = new JarOutputStream(Files.newOutputStream(Paths.get(outPath)));
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

        if (mode == 4)
            return;

        OutputStreamWriter writer = new OutputStreamWriter(Files.newOutputStream(Paths.get(outPath + ".jbl")));
        GsonBuilder builder = new GsonBuilder();
        builder.setPrettyPrinting();
        builder.create().toJson(blastLayer, writer);
        writer.close();
    }
}
