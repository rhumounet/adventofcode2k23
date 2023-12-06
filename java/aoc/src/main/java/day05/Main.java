package day05;

import java.io.FileNotFoundException;
import java.util.ArrayList;
import java.util.Arrays;
import java.util.Comparator;
import java.util.Hashtable;
import java.util.List;
import java.util.concurrent.atomic.AtomicInteger;
import java.util.stream.Collectors;

import day05.model.Range;
import day05.model.RangeP2;
import day05.model.SeedRange;
import utils.FileUtils;

public class Main {
	private static final String[] keys = new String[] {
			"seed-to-soil",
			"soil-to-fertilizer",
			"fertilizer-to-water",
			"water-to-light",
			"light-to-temperature",
			"temperature-to-humidity",
			"humidity-to-location"
	};

	public static void main(String[] args) throws FileNotFoundException {
		System.out.println(part1());
		System.out.println(part2());
		System.out.println(part2withBrain());
	}

	public static String part1() throws FileNotFoundException {
		var lines = FileUtils.LoadFile("day05/day05.txt");
		var seeds = getSeeds(lines);
		var collections = parseInput(lines);
		var lowest = getLowest_p1(seeds, collections);
		return "" + lowest;
	}

	public static String part2() throws FileNotFoundException {
		var lines = FileUtils.LoadFile("day05/day05.txt");
		var seeds = getSeeds(lines);
		var collections = parseInput_p2(lines);
		var lowest = getLowest_p2(seeds, collections);

		return "" + lowest;
	}

	public static String part2withBrain() throws FileNotFoundException {
		var lines = FileUtils.LoadFile("day05/day05.txt");
		var seeds = getSeeds(lines);
		var collections = parseInput_p2(lines);

		final AtomicInteger counter = new AtomicInteger(0);
		var seedRanges = seeds.stream()
			.collect(Collectors.groupingBy(value -> {
				final int i = counter.getAndIncrement();
				return (i % 2 == 0) ? i : i - 1;
			}))
			.values()
			.stream()
			.map(a -> new SeedRange(a.get(0), a.get(0) + a.get(1) - 1))
			.collect(Collectors.toSet());
		
		var minValue = Long.MAX_VALUE;
		for (SeedRange seedRange : seedRanges) {
			var leastResistencePath = getLeastResistencePath(collections, seedRange, 0);
			var testmin = leastResistencePath.stream().map(x -> x.min).min(Comparator.naturalOrder()).get();
			minValue = testmin < minValue ? testmin : minValue;
		}
		return "" + minValue;
	}

	private static List<SeedRange> getLeastResistencePath(Hashtable<String, ArrayList<RangeP2>> collections,
			SeedRange seedRange, int depth) {
		var paths = collections.get(keys[depth])
			.stream()
			// .filter()
			.map(x -> x.includes(seedRange))
			.collect(Collectors.toList());
		if (depth == keys.length - 1) {
			return paths;
		} else {
			List<SeedRange> childPaths = new ArrayList<SeedRange>();
			for (SeedRange pathRange : paths) {
				childPaths = getLeastResistencePath(collections, pathRange, depth + 1);
			}
			return childPaths;
		}
	}

	private static List<Long> getSeeds(String[] lines) throws FileNotFoundException {
		return Arrays.asList(lines[0].split(":")[1]
				.split(" "))
				.stream()
				.filter(x -> !x.isBlank())
				.map(x -> Long.parseLong(x.strip()))
				.collect(Collectors.toList());
	}

	private static Hashtable<String, ArrayList<Range>> parseInput(String[] lines) {
		var collections = new Hashtable<String, ArrayList<Range>>();
		var keepCrawling = false;
		var currentCollection = "";
		for (int i = 2; i < lines.length; i++) {
			if (lines[i].isBlank()) {
				keepCrawling = false;
				continue;
			}
			if (!keepCrawling) {
				keepCrawling = true;
				currentCollection = lines[i].split(" ")[0].strip();
				collections.put(currentCollection, new ArrayList<Range>());
				continue;
			}
			if (keepCrawling) {
				var split = Arrays.asList(lines[i].split(" "))
						.stream()
						.map(x -> Long.parseLong(x.strip()))
						.collect(Collectors.toList());
				collections.get(currentCollection).add(new Range(split.get(0), split.get(1), split.get(2)));
			}
		}
		return collections;
	}
	
	private static Long getLowest_p1(List<Long> seeds, Hashtable<String, ArrayList<Range>> collections) {
		var min = Long.MAX_VALUE;
		for (long seed : seeds) {
			var humidityToLocation = getHumidityToLocation(collections, seed);

			min = humidityToLocation < min ? humidityToLocation : min;
		}
		return min;
	}

	private static long getHumidityToLocation(Hashtable<String, ArrayList<Range>> collections, long seed) {
		var seedToSoil = collections.get(keys[0])
				.stream()
				.filter(x -> x.isInSource(seed))
				.findFirst()
				.orElse(new Range(true))
				.target(seed);

		var soilToFertilizer = collections.get(keys[1])
				.stream()
				.filter(x -> x.isInSource(seedToSoil))
				.findFirst()
				.orElse(new Range(true))
				.target(seedToSoil);

		var fertilizerToWater = collections.get(keys[2])
				.stream()
				.filter(x -> x.isInSource(soilToFertilizer))
				.findFirst()
				.orElse(new Range(true))
				.target(soilToFertilizer);

		var waterToLight = collections.get(keys[3])
				.stream()
				.filter(x -> x.isInSource(fertilizerToWater))
				.findFirst()
				.orElse(new Range(true))
				.target(fertilizerToWater);

		var lightToTemp = collections.get(keys[4])
				.stream()
				.filter(x -> x.isInSource(waterToLight))
				.findFirst()
				.orElse(new Range(true))
				.target(waterToLight);

		var tempToHumidity = collections.get(keys[5])
				.stream()
				.filter(x -> x.isInSource(lightToTemp))
				.findFirst()
				.orElse(new Range(true))
				.target(lightToTemp);

		var humidityToLocation = collections.get(keys[6])
				.stream()
				.filter(x -> x.isInSource(tempToHumidity))
				.findFirst()
				.orElse(new Range(true))
				.target(tempToHumidity);
		return humidityToLocation;
	}

	private static Hashtable<String, ArrayList<RangeP2>> parseInput_p2(String[] lines) {
		var collections = new Hashtable<String, ArrayList<RangeP2>>();
		var keepCrawling = false;
		var currentCollection = "";
		for (int i = 2; i < lines.length; i++) {
			if (lines[i].isBlank()) {
				keepCrawling = false;
				continue;
			}
			if (!keepCrawling) {
				keepCrawling = true;
				currentCollection = lines[i].split(" ")[0].strip();
				collections.put(currentCollection, new ArrayList<RangeP2>());
				continue;
			}
			if (keepCrawling) {
				var split = Arrays.asList(lines[i].split(" "))
						.stream()
						.map(x -> Long.parseLong(x.strip()))
						.collect(Collectors.toList());
				collections.get(currentCollection).add(new RangeP2(split.get(0), split.get(1), split.get(2)));
			}
		}
		return collections;
	}

	private static Long getLowest_p2(List<Long> seeds, Hashtable<String, ArrayList<RangeP2>> collections) {
		var min = Long.MAX_VALUE;
		final AtomicInteger counter = new AtomicInteger(0);
		var totalSeeds = seeds.stream()
				.collect(Collectors.groupingBy(value -> {
					final int i = counter.getAndIncrement();
					return (i % 2 == 0) ? i : i - 1;
				}))
				.values()
				.stream()
				.map(a -> new SeedRange(a.get(0), a.get(0) + a.get(1) - 1))
				.collect(Collectors.toSet());
		for (SeedRange seedRange : totalSeeds) {
			var humidityToLocation = getHumidityToLocation_p2(collections, seedRange, 0);
			min = humidityToLocation < min ? humidityToLocation : min;
		}
		return min;
	}

	private static Long getHumidityToLocation_p2(Hashtable<String, ArrayList<RangeP2>> collections, SeedRange seedRange,
			int keyIndex) {
		var seeds = collections.get(keys[keyIndex])
				.stream()
				.map(x -> x.includes(seedRange))
				.filter(x -> x != null)
				.collect(Collectors.toList());
		if (keyIndex == keys.length - 1) {
			return seeds.stream().map(x -> x.min).min(Comparator.naturalOrder()).get();
		} else {
			var list = new ArrayList<Long>();
			for (SeedRange seedRange2 : seeds) {
				list.add(getHumidityToLocation_p2(collections, seedRange2, keyIndex + 1));
			}
			return list.stream().min(Comparator.naturalOrder()).get();
		}
	}

}