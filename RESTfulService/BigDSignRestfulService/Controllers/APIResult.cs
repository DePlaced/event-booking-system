namespace BigDSignRestfulService.Controllers
{
    /// <summary>
    /// An API result that can contain data or an error message.
    /// </summary>
    public class APIResult
    {
        /// <summary>
        /// Create an empty API result.
        /// </summary>
        /// <returns>The created API result.</returns>
        public static APIResult Empty()
        {
            return new APIResult();
        }

        /// <summary>
        /// Create an API result containing data.
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static APIResult WithData(object data)
        {
            APIResult apiResult = new APIResult();
            apiResult.Data = data;
            return apiResult;
        }

        /// <summary>
        /// Create an API result containing an error message.
        /// </summary>
        /// <param name="error">The error message.</param>
        /// <returns>The created API result.</returns>
        public static APIResult WithError(string error)
        {
            APIResult apiResult = new APIResult();
            apiResult.Error = error;
            return apiResult;
        }

        /// <summary>
        /// Create an API result containing an internal server error.
        /// </summary>
        /// <returns>The created API result.</returns>
        public static APIResult WithInternalServerError()
        {
            APIResult apiResult = new APIResult();
            apiResult.Error = "Internal server error.";
            return apiResult;
        }

        /// <summary>
        /// The data.
        /// </summary>
        public object? Data { get; set; }
        /// <summary>
        /// The error message.
        /// </summary>
        public string? Error { get; set; }
    }
}
