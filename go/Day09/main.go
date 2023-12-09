package main

import (
	"os"
	"strconv"
	"strings"

	"github.com/ahmetalpbalkan/go-linq"
)

func check(e error) {
	if e != nil {
		panic(e)
	}
}

func part1() int {
	input, err := os.ReadFile("Day09/input.txt")
	check(err)
	content := strings.Split(string(input), "\n")
	value := linq.From(content).SelectT(func(s string) int {
		var currentNumbers []int
		//initial convert
		linq.From(strings.Split(s, " ")).SelectT(convertToInt).ToSlice(&currentNumbers)
		allNumbers := [][]int{
			currentNumbers,
		}
		for linq.From(currentNumbers).AnyWithT(func(i int) bool {
			return i != 0
		}) {
			temp := make([]int, len(currentNumbers)-1)
			for i := 0; i < len(currentNumbers)-1; i++ {
				temp[i] = currentNumbers[i+1] - currentNumbers[i]
			}
			allNumbers = append(allNumbers, temp)
			currentNumbers = temp
		}
		for i := len(allNumbers) - 1; i >= 0; i-- {
			if i == len(allNumbers)-1 {
				allNumbers[i] = append(allNumbers[i], 0)
			} else {
				allNumbers[i] = append(allNumbers[i], allNumbers[i][len(allNumbers[i])-1]+allNumbers[i+1][len(allNumbers[i+1])-1])
			}
		}
		result := allNumbers[0][len(allNumbers[0])-1]
		return result
	}).Aggregate(func(x interface{}, y interface{}) interface{} {
		return x.(int) + y.(int)
	})
	return value.(int)
}

func part2() int {
	input, err := os.ReadFile("Day09/input.txt")
	check(err)
	content := strings.Split(string(input), "\n")
	value := linq.From(content).SelectT(func(s string) int {
		var currentNumbers []int
		//initial convert
		linq.From(strings.Split(s, " ")).SelectT(convertToInt).ToSlice(&currentNumbers)
		allNumbers := [][]int{
			currentNumbers,
		}
		for linq.From(currentNumbers).AnyWithT(func(i int) bool {
			return i != 0
		}) {
			temp := make([]int, len(currentNumbers)-1)
			for i := 0; i < len(currentNumbers)-1; i++ {
				temp[i] = currentNumbers[i+1] - currentNumbers[i]
			}
			allNumbers = append(allNumbers, temp)
			currentNumbers = temp
		}
		for i := len(allNumbers) - 1; i >= 0; i-- {
			if i == len(allNumbers)-1 {
				allNumbers[i] = append([]int{0}, allNumbers[0]...)
			} else {
				allNumbers[i] = append([]int{allNumbers[i][0] - allNumbers[i+1][0]}, allNumbers[i]...)
			}
		}
		result := allNumbers[0][0]
		return result
	}).Aggregate(func(x interface{}, y interface{}) interface{} {
		return x.(int) + y.(int)
	})
	return value.(int)
}

func convertToInt(s string) int {
	i, err := strconv.Atoi(strings.TrimSpace(s))
	check(err)
	return i
}

func main() {
	println(part1())
	println(part2())
}
