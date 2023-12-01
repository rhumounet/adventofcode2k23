import regex as re

with open("day_01/input.txt", "r") as f:
    lines = f.readlines()

def part1():
    sum = 0
    for l in lines:
        m = re.findall(r"\d", l)
        size = len(m)
        str = ""
        if (size):
            str += m[0]
            if (size > 1):
                str += m[size - 1]
            else:
                str += m[0]
        if (str):
            sum += int(str)
    return sum

def part2():
    sum = 0
    dict = {
        "one": "1", "two": "2", "three": "3", "four": "4", "five": "5", "six": "6","seven": "7", "eight": "8", "nine": "9"
    }
    exp = r"(\d|" + "|".join(f'{x}' for x in dict.keys()) + ")"
    for l in lines:
        m = re.findall(exp, l, overlapped=True)
        size = len(m)
        str = ""
        if (size):
            first = m[0]
            str += first if len(first) == 1 else dict[first]
            if (size > 1):
                last = m[size - 1]
                str += last if len(last) == 1 else dict[last]
            else:
                str += str
        if (str):
            sum += int(str)
    return sum

print("part1: " + str(part1()))
print("part2: " + str(part2()))