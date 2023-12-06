package day05.model;

public class Range {
    public long destination;
    public long source;
    public long width;
    private boolean override;
    public Range(boolean override) {
        this.override = override;
    }
    public Range(long destination, long source, long width) {
        super();
        this.destination = destination;
        this.source = source;
        this.width = width;
    }
    public boolean isInSource(final long value) {
        var isInSource = source <= value && source + width >= value;
        return isInSource;
    }
    public long target(final long value) {
        if (override)
            return value;
        return value - source + destination;
    }

    public String toString() {
        return "Source " + source + ", Destination " + destination + ", Width " + width;
    }
}
