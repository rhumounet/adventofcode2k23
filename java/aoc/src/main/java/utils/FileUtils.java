package utils;

import java.io.File;
import java.io.FileNotFoundException;
import java.util.ArrayList;
import java.util.List;
import java.util.Scanner;

public class FileUtils {
    public static String[] LoadFile(String path) throws FileNotFoundException {
		Scanner scanner = new Scanner(new File(path));
		scanner.useDelimiter("\r\n");
		List<String> lines = new ArrayList<>();
		while (scanner.hasNext()) {
			lines.add(scanner.next());
		}
		scanner.close();
		return lines.toArray(new String[lines.size()]);
    }
}