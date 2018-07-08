using System;

namespace Maya.Tinker
{
    public class SQLResults
    {
        public int WasSuccessful { get; set; }

        public string Message { get; set; }



        public static SQLResults AwaitingApproval()
        {
            return new SQLResults
            {
                WasSuccessful = -99,
                Message = "Awaiting Approval"
            };
        }


        public static SQLResults DuplicateEntry()
        {
            return new SQLResults
            {
                WasSuccessful = -4,
                Message = "Duplicate Entry"
            };
        }

        public static SQLResults NotFound()
        {
            return new SQLResults
            {
                WasSuccessful = -3,
                Message = "Couldn't find a record with this ID"
            };
        }

        public static SQLResults CouldNotDeserialize()
        {
            return new SQLResults
            {
                WasSuccessful = -2,
                Message = "Couldn't deserialize our JSON string"
            };
        }

        public static SQLResults Error(Exception ex)
        {
            return new SQLResults
            {
                WasSuccessful = -1,
                Message = ex.ToString()
            };
        }

        public static SQLResults Success()
        {
            return new SQLResults
            {
                WasSuccessful = 0,
                Message = "Success"
            };
        }
    }
}