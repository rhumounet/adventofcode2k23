import regex as re
import math
import numpy
from PIL import Image as im

with open("day_10/input.txt", "r") as f:
    content = f.read()
    lines = content.split("\n")
    maxX = len(lines[0])
    maxY = len(lines)

def part1():
    match = re.search(r"S", content)
    y0 = math.ceil(match.regs[0][0]/len(lines[0])) - 1
    starting = lines[y0]
    x0 = starting.index("S")
    #assert which piece is starting point
    dir, first, second = getInitialPipe(x0, y0)
    [firstDir, secondDir] = dir
    #get next pipes
    dist = 1
    while first[0] != second[0] or first[1] != second[1]:
        #first way
        firstDir, first = getNext(firstDir, first)
        #second way
        secondDir, second = getNext(secondDir, second)
        dist += 1
    return dist

def part2():
    
    match = re.search(r"S", content)
    y0 = math.ceil(match.regs[0][0]/len(lines[0])) - 1
    starting = lines[y0]
    x0 = starting.index("S")
    #assert which piece is starting point
    dir, first, second = getInitialPipe(x0, y0)
    [firstDir, secondDir] = dir
    #get next pipes
    dist = 1
    totalPointsA = []
    totalPointsB = []
    while first[0] != second[0] or first[1] != second[1]:
        #first way
        firstDir, first = getNext(firstDir, first)
        #second way
        secondDir, second = getNext(secondDir, second)
        #shapely
        # firstPoint = Point(first[0], first[1])
        # secondPoint = Point(second[0], second[1])
        totalPointsA.append(first)
        totalPointsB.append(second)
        dist += 1
    totalPointsB.reverse()
    newArray = totalPointsA + totalPointsB
    realArray = numpy.full(len(lines[0]) * len(lines), fill_value=0, dtype=numpy.uint8)
    realArray = numpy.reshape(realArray, (len(lines), len(lines[0])))
    for (x1, y1) in newArray:
        realArray[y1][x1] = 255
    data = im.fromarray(realArray)
    data.save('test.png')
    # isolated = 0
    # for x in range(len(lines[0])):
    #     for y in range(len(lines)):
    #         isolated += 1
    return ''

def getInitialPipe(x: int, y: int):
    adj = [
        orientation(lines[y-1][x]) if y - 1 >= 0 else '  ',
        orientation(lines[y][x + 1]) if x + 1 < maxX else '  ',
        orientation(lines[y + 1][x]) if y + 1 < maxY else '  ',
        orientation(lines[y][x - 1]) if x - 1 >= 0 else '  ',
    ]
    if (adj[0][0] == 'S' or adj[0][1] == 'S'):
        if (adj[1][1] == 'W'):
            return ('NE', (x, y - 1), (x + 1, y))
        if (adj[2][0] == 'N'):
            return ('NS', (x, y - 1), (x, y + 1))
        if (adj[3][1] == 'E'):
            return ('SW', (x, y + 1), (x - 1, y))
    if (adj[1][0] == 'W' or adj[1][1] == 'W'):
        if (adj[2][0] == 'N'):
            return ('SE', (x, y + 1), (x + 1, y))
        if (adj[3][1] == 'E'):
            return ('WE', (x - 1, y), (x + 1, y))
    if (adj[2][0] == 'N'):
        if (adj[3][1] == 'E'):
            return ('SW', (x, y + 1), (x - 1, y))
    
    return ('', (0, 0), (0, 0))

def getNext(dir: str, current: tuple[int, int]):
    oCurrent = orientation(lines[current[1]][current[0]])
    match dir:
        case 'N':
            match oCurrent:
                case 'NS':
                    return ('N', (current[0], current[1] - 1))
                case 'SE':
                    return ('E', (current[0] + 1, current[1]))
                case 'SW':
                    return ('W', (current[0] - 1, current[1]))
        case 'E':
            match oCurrent:
                case 'WE':
                    return ('E', (current[0] + 1, current[1]))
                case 'NW':
                    return ('N', (current[0], current[1] - 1))
                case 'SW':
                    return ('S', (current[0], current[1] + 1))
        case 'W':
            match oCurrent:
                case 'WE':
                    return ('W', (current[0] - 1, current[1]))
                case 'NE':
                    return ('N', (current[0], current[1] - 1))
                case 'SE':
                    return ('S', (current[0], current[1] + 1))
        case 'S':
            match oCurrent:
                case 'NS':
                    return ('S', (current[0], current[1] + 1))
                case 'NW':
                    return ('W', (current[0] - 1, current[1]))
                case 'NE':
                    return ('E', (current[0] + 1, current[1]))

def orientation(char: str):
    match char:
        case '-':
            return 'WE'
        case '|':
            return 'NS'
        case 'F':
            return 'SE'
        case 'J':
            return 'NW'
        case 'L':
            return 'NE'
        case '7':
            return 'SW'
        case _:
            return 'XX'
    
# print(part1())
print(part2())