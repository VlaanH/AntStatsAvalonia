
namespace AntStatsCore.Parsing
{
    public static class StandardData
    {
        public static ParsingObject StandardParsingObject = new ParsingObject()
        {
            MainTableParsingPattern = @"<legend>AntMiner</legend>([\w \W ]+)<div class=""cbi-section-node"" style=""margin-top:8px;"">",
            MainNameTable = "//table[@class='cbi-section-table']",
            AdditionalTableParsingPattern = @"<legend>Summary</legend>([\w \W ]+)<legend>Pools</legend>",
            AdditionalNameTable = "//table[@class='cbi-section-table']"
            
        };
        
    }
    
}