import regex as re

with open("day_02/input.txt", "r") as f:
    lines = f.readlines()

def part1():
    id = r"Game (\d*)\:"
    game = r"(\d+)\s(red|green|blue)"
    goal = {
        "red": 12, "green": 13, "blue": 14
    }
    sum = 0
    for l in lines:
        idm = re.findall(id, l)[0]
        possible = True
        for split in l.split(';'):
            dict = {
                "red": 0, "green": 0, "blue": 0
            }
            m = re.findall(game, split)
            for match in m:
                dict[match[1]] += int(match[0])
            if (goal["red"] < dict["red"] or goal["blue"] < dict["blue"] or goal["green"] < dict["green"]):
                possible = False
                break
        if (possible):
            sum += int(idm)
    return sum

def part2():
    id = r"Game (\d*)\:"
    game = r"(\d+)\s(red|green|blue)"
    sum = 0
    for l in lines:
        idm = re.findall(id, l)[0]
        possible = True
        min = {
            "red": 0,
            "green": 0,
            "blue": 0
        }
        for split in l.split(';'):
            m = re.findall(game, split)
            for match in m:
                min[match[1]] = int(match[0]) if min[match[1]] < int(match[0]) else min[match[1]]
        val = 1
        for key in min.keys():
            val *= min[key]
        sum += val
    return sum

print(part1())
print(part2())