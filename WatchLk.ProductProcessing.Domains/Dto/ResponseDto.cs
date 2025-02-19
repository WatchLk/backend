namespace WatchLk.ProductProcessing.Domains.Dto

{
    public record ResponseDto
    (
        int Status,
        Object? Result,
        string? Error
    );
}
