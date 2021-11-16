using Gentrack_JagmeetPOC.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace Gentrack_JagmeetPOC
{
    public class Validator : IValidator
    {
        public void Validate(IList<string> content)
        {
            //
            if (!content.Any())
            {
                throw new ValidationException("No data in CSVIntervalData");
            }

            
            if (content.Count < 4)
            {
                throw new ValidationException("At least 4 lines required to generate proper csv's");
            }
            ValidateCase1(content);
            ValidateCase2(content);
            ValidateCase3(content);
            ValidateCase4(content);
        }

        /// <summary>
        /// The CSVIntervalData element should contain at least 1 row for each of "100", "200", "300","900"
        /// </summary>
        /// <param name="content"></param>
        private void ValidateCase1(IList<string> content)
        {
            //var lstToValidate = new List<string> {"100", "200", "300", "900"};
            if (content.Count(x => x.StartsWith("100") || x.StartsWith("200") || x.StartsWith("300") || x.StartsWith("900")) !=content.Count)
            {
                throw new ValidationException("The CSVIntervalData element should contain at least 1 row for each of '100', '200', '300','900'");
            }
        }

        /// <summary>
        /// "100", "900" rows should only appear once inside the CSVIntervalData element
        /// </summary>
        private void ValidateCase2(IList<string> content)
        {
            if (content.Count(x => x.StartsWith("100"))!=1 || content.Count(x => x.StartsWith("900")) != 1)
            {
                throw  new ValidationException("'100', '900' rows should only appear once inside the CSVIntervalData element");
            }
        }

        /// <summary>
        /// "200" and "300" can repeat and will be within the header and trailer rows 
        /// </summary>
        private void ValidateCase3(IList<string> content)
        {
            if (content.First().StartsWith("200") || content.First().StartsWith("300"))
            {
                throw new ValidationException("'200' and '300' can repeat and will be within the header and trailer rows ");
            }
            if (content.Last().StartsWith("200") || content.Last().StartsWith("300"))
            {
                throw new ValidationException("'200' and '300' can repeat and will be within the header and trailer rows ");
            }
        }

        /// <summary>
        /// "200" row must be followed by at least 1 "300" row
        /// </summary>
        private void ValidateCase4(IList<string> content)
        {
            for (int i = 0; i < content.Count; i++)
            {
                if (content[i].StartsWith("200") && !content[i + 1].StartsWith("300"))
                {
                    throw new ValidationException("'200' row must be followed by at least 1 '300' row");
                }
            }
        }

        
    }
}
