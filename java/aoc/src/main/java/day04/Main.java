package day04;

import java.io.FileNotFoundException;
import java.util.Arrays;
import java.util.Hashtable;
import java.util.List;
import java.util.Set;
import java.util.stream.Collectors;

import utils.FileUtils;

public class Main {

	public static void main(String[] args) throws FileNotFoundException {
		System.out.println(part1());
		System.out.println(part2());
	}

	public static String part1() throws FileNotFoundException {
		var lines = FileUtils.LoadFile("day04/day04.txt");
		int sum = 0;
		for (String line : lines) {
			var parts = line.split(":")[1].split("\\|");
			List<Integer> winning = Arrays.asList(parts[0].split(" "))
				.stream()
				.filter(x -> !x.isBlank())
				.map(x -> Integer.parseInt(x)).toList();
			List<Integer> mine = Arrays.asList(parts[1].split(" "))
				.stream()
				.filter(x -> !x.isBlank())
				.map(x -> Integer.parseInt(x.strip())).toList();

			Set<Integer> result = mine.stream()
				.distinct()
				.filter(winning::contains)
				.collect(Collectors.toSet());
			
			int score = 0;
			for (Integer value : result) {
				if (score == 0) {
					score = 1;
				} else {
					score *= 2;
				}
			}
			sum += score;
		}
		return Integer.toString(sum);
	}

	public static String part2() throws FileNotFoundException {
		var lines = FileUtils.LoadFile("day04/day04.txt");
		Hashtable<Integer, Integer> sums = new Hashtable<>();
		for (int i = 0; i < lines.length; i++) {
			sums.put(i, 1);
		}
		for (int i = 0; i < lines.length; i++) {
			var line = lines[i];

			var parts = line.split(":")[1].split("\\|");
			List<Integer> winning = Arrays.asList(parts[0].split(" "))
				.stream()
				.filter(x -> !x.isBlank())
				.map(x -> Integer.parseInt(x)).toList();

			List<Integer> mine = Arrays.asList(parts[1].split(" "))
				.stream()
				.filter(x -> !x.isBlank())
				.map(x -> Integer.parseInt(x.strip())).toList();

			long result = mine.stream()
				.distinct()
				.filter(winning::contains)
				.count();
			if (result > 0) {
				for (int k = 0; k < sums.get(i); k++) {
					for (int j = i + 1; j <= i + result; j++) {
						sums.put(j, sums.get(j) + 1);
					}
				}
			}
		}
		return Integer.toString(sums.values().stream().mapToInt(Integer::intValue).sum());
	}

}
