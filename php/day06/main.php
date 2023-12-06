<?php 
class Main {
    public function part1() {
        $handle = fopen("input.txt","r");
        //first line
        $line = fgets($handle);
        $first = array_filter(explode(" ", $line), function($s) {
            return intval(trim($s)) != 0;
        });
        //second line
        $line = fgets($handle);
        $second = array_filter(explode(" ", $line), function($s) {
            return intval(trim($s)) != 0;
        });
        fclose($handle);
        $times = [];
        foreach($first as $time) {
            array_push($times, intval($time));
        }
        $distances = [];
        foreach($second as $distance) {
            array_push($distances, intval($distance));
        }
        $totalWays = [];
        for ($i=0; $i < count($times); $i++) { 
            $t = 0;
            $tMax = $times[$i];
            $v = 0;
            $ways = 0;
            $target = $distances[$i];
            while(($tMax - $t)*$v < $target) {
                $t++;
                $v++;
            }
            while(($tMax - $t)*$v > $target) {
                $t++;
                $v++;
                $ways++;
            }
            array_push($totalWays, $ways);
        }
        $total = 1;
        foreach ($totalWays as $w) {
            $total *= $w;
        }
        return $total;
    }

    public function part2() {
        $handle = fopen("input.txt","r");
        //first line
        $line = fgets($handle);
        $first = array_filter(explode(" ", $line), function($s) {
            return intval(trim($s)) != 0;
        });
        //second line
        $line = fgets($handle);
        $second = array_filter(explode(" ", $line), function($s) {
            return intval(trim($s)) != 0;
        });
        fclose($handle);
        $totalTime = intval(array_reduce($first, function($carry, $item) {
            return  $carry.$item;
        }, ""));
        $totalDistance = intval(array_reduce($second, function($carry, $item) {
            return $carry.$item;
        }, ""));
        $t = 0;
        $v = 0;
        $ways = 0;
        while(($totalTime - $t)*$v < $totalDistance) {
            $t++;
            $v++;
        }
        while(($totalTime - $t)*$v > $totalDistance) {
            $t++;
            $v++;
            $ways++;
        }
        return $ways;
    }
}
$main = new Main();
echo $main->part1();
echo $main->part2();
?>