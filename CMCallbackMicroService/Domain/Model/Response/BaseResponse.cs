namespace AccountManagementMicroService.Domain.Model.Response
{
    public class BaseResponse<T>
    {
        public bool Success { get; set; }
        public ResponseCode ResponseCodeDetails { get; set; }
        public T? Data { get; set; }

    }
    public class BaseResponseChannel<T>
    {
        public Boolean success { get; set; } = true;
        public String responseCode { get; set; } = "00";
        public String responseMessage { get; set; } = "data fetch successfully";
        public String requestingOrganisationTransactionReference { get; set; }
        public T responseData { get; set; }
    }
    public class ResponseCode
    {
        public Int64 Id { get; set; }
        public string? ThirdPartyCode { get; set; }
        public string? ThirdPartyMessage { get; set; }
        public Int64 ChannelId { get; set; }
        public string? ResponseCodeID { get; set; }
        public List<ResponseCodeDetails>? ResponseCodeDetailList { get; set; }
    }
    public class ResponseCodeDetails
    {
        public string? ResponseMessgae { get; set; }
        public Int64 ResponseCodeId { get; set; }
        public Int64 LangaugeId { get; set; }
    }

    public class BaseResponses<T>
    {
        public bool Success { get; set; }
        public string responseMessage { get; set; }
        public T? Data { get; set; }

    }

    public class BaseResponseConfig<T>
    {
        public Boolean success { get; set; } = true;
        public String responseCode { get; set; } = "00";
        public String responseMessage_en { get; set; } = "data fetch successfully";
        public String responseMessage_fr { get; set; } = "data fetch successfully";
        public String requestingOrganisationTransactionReference { get; set; }
        public T responseData { get; set; }
    }
}
