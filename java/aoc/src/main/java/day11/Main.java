package day11;

import java.io.FileNotFoundException;
import java.util.ArrayList;
import java.util.List;

import utils.FileUtils;

public class Main {

	public static void main(String[] args) throws FileNotFoundException {
		System.out.println(part1());
		System.out.println(part2());
	}

	public static String part1() throws FileNotFoundException {
		var lines = FileUtils.LoadFile("day11/input.txt");
		var newLines = parseInput(lines);
		var galaxies = new ArrayList<Galaxy>();
		for (int i = 0; i < newLines.length; i++) {
			for (int j = 0; j < newLines[i].length(); j++) {
				if (newLines[i].charAt(j) == '#') {
					galaxies.add(new Galaxy(i, j));
				}
			}
		}
		int sum = 0;
		for (Galaxy galaxy : galaxies) {
			int currentGalaxyIndex = galaxies.indexOf(galaxy);
			while (currentGalaxyIndex < galaxies.size() - 1) {
				var next = galaxies.get(currentGalaxyIndex + 1);
				sum += Math.abs(galaxy.x - next.x) + Math.abs(galaxy.y - next.y);
				currentGalaxyIndex++;
			}
		}
		return Integer.toString(sum);
	}

	public static String part2() throws FileNotFoundException {
		var lines = FileUtils.LoadFile("day11/input.txt");
		var warp = getWarps(lines);
		var rows = warp.rows;
		var columns = warp.columns;
		var galaxies = new ArrayList<Galaxy>();
		long expansion = 999999;
		for (int i = 0; i < lines.length; i++) {
			for (int j = 0; j < lines[i].length(); j++) {
				if (lines[i].charAt(j) == '#') {
					final int col = j;
					final int row = i;
					long nbColumnsExpansions = columns
							.stream()
							.filter(x -> x < col)
							.count();
					long nbRowExpansions = rows
							.stream()
							.filter(y -> y < row)
							.count();
					galaxies.add(new Galaxy(j + (expansion * nbColumnsExpansions), i + (expansion * nbRowExpansions)));
				}
			}
		}
		long sum = 0;
		for (Galaxy galaxy : galaxies) {
			int currentGalaxyIndex = galaxies.indexOf(galaxy) + 1;
			while (currentGalaxyIndex < galaxies.size()) {
				var next = galaxies.get(currentGalaxyIndex);
				long realxdiff, realydiff;
				realxdiff = Math.abs(next.x - galaxy.x);
				realydiff = Math.abs(next.y - galaxy.y);
				sum += realxdiff + realydiff;
				currentGalaxyIndex++;
			}
		}
		return Long.toString(sum);
	}

	private static String[] parseInput(String[] lines) {
		var warp = getWarps(lines);
		var rows = warp.rows;
		var columns = warp.columns;
		var newLines = new String[lines.length + rows.size()];
		var span = 0;
		var rowToAdd = new String(new char[lines[0].length()]).replace("\0", ".");
		for (int i = 0; i < lines.length; i++) {
			newLines[i + span] = lines[i];
			if (rows.contains(i)) {
				newLines[i + 1 + span] = rowToAdd;
				span++;
			}
		}
		int columnIndex;
		int substringIndex;
		for (int i = 0; i < newLines.length; i++) {
			span = 0;
			for (int j = 0; j < columns.size(); j++) {
				columnIndex = columns.get(j);
				substringIndex = columnIndex + 1 + span;
				newLines[i] = newLines[i].substring(0, substringIndex)
						+ "."
						+ newLines[i].substring(substringIndex);
				span++;
			}
		}
		return newLines;
	}

	private static Warp getWarps(String[] lines) {
		var columns = new ArrayList<Integer>();
		var rows = new ArrayList<Integer>();
		int index;
		boolean isEmpty;
		for (int i = 0; i < lines[0].length(); i++) {
			isEmpty = true;
			index = 0;
			while (index < lines.length) {
				if (lines[index].charAt(i) != '.') {
					isEmpty = false;
					break;
				}
				index++;
			}
			if (isEmpty) {
				columns.add(i);
			}
		}
		for (int i = 0; i < lines.length; i++) {
			isEmpty = true;
			index = 0;
			while (index < lines.length) {
				if (lines[i].charAt(index) != '.') {
					isEmpty = false;
					break;
				}
				index++;
			}
			if (isEmpty) {
				rows.add(i);
			}
		}
		return new Warp(rows, columns);
	}

	public static class Warp {
		public List<Integer> rows;
		public List<Integer> columns;

		public Warp(List<Integer> rows, List<Integer> columns) {
			super();
			this.rows = rows;
			this.columns = columns;
		}
	}

	public static class Galaxy {
		public long x;
		public long y;

		public Galaxy(long x, long y) {
			super();
			this.x = x;
			this.y = y;
		}
	}

}
