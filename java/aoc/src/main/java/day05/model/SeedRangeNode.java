package day05.model;

public class SeedRangeNode {
    public SeedRange input;
    public SeedRange output;
    public SeedRangeNode(SeedRange input, SeedRange output) {
        this.input = input;
        this.output = output;
    }

    public boolean canProcess(SeedRange range) {
        return input.intersect(range) != null;
    } 

    public SeedRange transform(SeedRange range) {
        return output.intersect(range);
    }
}
