package day05.model;

public class RangeP2 {
    public SeedRange destination;
    public SeedRange source;

    public RangeP2(long destination, long source, long width) {
        super();
        this.destination = new SeedRange(destination, destination + width);
        this.source = new SeedRange(source, source + width);
    }

    public long getMinSource() {
        return source.min;
    }

    public long getMinDestination() {
        return destination.min;
    }

    public SeedRange includes(SeedRange seedRange) {
        var source_intersection = source.intersect(seedRange);
        if (source_intersection == null) {
            return seedRange;
        }
        return destination.translate(source_intersection);
    }

    public boolean sourceIncludes(SeedRange seedRange) {
        return source.intersect(seedRange) != null;
    }

    public boolean destinationIncludes(SeedRange seedRange) {
        return destination.intersect(seedRange) != null;
    }
}
