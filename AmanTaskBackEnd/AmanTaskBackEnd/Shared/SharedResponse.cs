namespace AmanTaskBackEnd.Shared
{
    public class SharedResponse<T>
    {
        public SharedResponse(Status status, T? data, string message = "")
        {
            this.message=message;
            this.status=status;
            this.data=data;
        }
        public string message { get; set; }
        public T? data { get; set; }
        public Status status { get; set; }
    }

    public enum Status { notFound, badRequest, noContent, problem, createdAtAction, found, expeption }
}
