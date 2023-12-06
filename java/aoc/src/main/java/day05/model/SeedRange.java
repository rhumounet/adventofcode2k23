package day05.model;

public class SeedRange {
    public long min;
    public long max;

    public SeedRange(long min, long max) {
        super();
        this.min = min;
        this.max = max;
    }

    public SeedRange intersect(SeedRange other) {
        if (other.max >= this.min && other.min <= this.max) {
            var _min = Math.max(this.min, other.min);
            var _max = Math.min(this.max, other.max);
            return new SeedRange(_min, _max);
        }
        return null;
    }

    public SeedRange translate(SeedRange other) {
        var delta = other.max - other.min;
        return new SeedRange(min, min + delta);
    }
}
