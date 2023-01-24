using Google.Apis.Sheets.v4;
using Google.Apis.Sheets.v4.Data;
using Sankirtana.Web.Common;

namespace BudgetUpdaterApp;

public class SheetOperationService
{
    const string SPREADSHEET_ID = "1_7EVuWmnobK6jDQDUUKh7a1zJwW4_6DrhH4VKNiNwDE";
    const string SHEET_NAME = "Итоговые данные";
    
    GoogleSheetsHelper _googleSheetsHelper;

    public SheetOperationService(string googleCredsJson)
    {
        _googleSheetsHelper = new GoogleSheetsHelper(googleCredsJson);
        
    }

    public string GetValues()
    {
        var range = $"{SHEET_NAME}!A:B";
        var googleSheetValues = _googleSheetsHelper.Service.Spreadsheets.Values;
        var request = googleSheetValues.Get(SPREADSHEET_ID, range);
        var response = request.Execute();
        var values = response.Values;
        
        return values.Count.ToString();
    }
    
    public void SetTinkoffBalanceOnSheet(string value)
    {
      
        var range2 = $"{SHEET_NAME}!C{2}:C{2}";
        var valueRange = new ValueRange
        {
            Values = new List<IList<object>>{new List<object> {value}}
        };
        var googleSheetValues = _googleSheetsHelper.Service.Spreadsheets.Values;
        var updateRequest = googleSheetValues.Update(valueRange, SPREADSHEET_ID, range2);
        updateRequest.ValueInputOption = SpreadsheetsResource.ValuesResource.UpdateRequest.ValueInputOptionEnum.USERENTERED;
        updateRequest.Execute();
    }
}