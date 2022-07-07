internal class Program
{
    private static void Main(string[] args)
    {

    }

    private void OnButtonClicked(object sender, EventArgs e)
    {
        string passportNumber = passportTextbox.Text.Trim();
        string rawData;
        int error1, error2;

        if (ValidatePassportTextBox(passportNumber, out int validationError, out rawData) == false)
        {
            error1 = validationError;
            return;
        }

        if (TryStorePassportData(rawData, out int connectionError) == false)
        {
            error2 = connectionError;
            return;
        }
    }

    private bool ValidatePassportTextBox(string passportNumber, out int validationError, out string rawData)
    {
        validationError = default;
        rawData = string.Empty;

        if (passportNumber == string.Empty)
        {
            validationError = (int)MessageBox.Show("Введите серию и номер паспорта");

            return false;
        }

        rawData = passportNumber.Replace(" ", string.Empty);

        if (rawData.Length < 10)
        {
            textResult.Text = "Неверный формат серии или номера паспорта";

            return false;
        }

        return true;
    }

    private bool TryStorePassportData(string rawData, out int connectionError)
    {
        string connectionString = string.Format("Data Source=" + Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\db.sqlite");
        var connection = OpenConnection(connectionString, out connectionError);

        if (connection == null)
            return false;

        string commandText = string.Format("select * from passports where num='{0}' limit 1;", (object)Form1.ComputeSha256Hash(rawData));
        SQLiteDataAdapter sqLiteDataAdapter = new SQLiteDataAdapter(new SQLiteCommand(commandText, connection));

        DataTable dataTable1 = new DataTable();
        sqLiteDataAdapter.Fill(dataTable1);

        ValidateDataTable(dataTable1);

        connection.Close();

        return true;
    }

    private SQLiteConnection? OpenConnection(string connectionString, out int connectionError)
    {
        SQLiteConnection? connection = null;
        connectionError = default;

        try
        {
            connection = new SQLiteConnection(connectionString);
            connection.Open();
        }
        catch (SQLiteException ex)
        {
            if (ex.ErrorCode != 1)
                return null;
            connectionError = (int)MessageBox.Show("Файл db.sqlite не найден. Положите файл в папку вместе с exe.");
        }

        return connection;
    }

    private void ValidateDataTable(DataTable dataTable)
    {
        if (dataTable.Rows.Count > 0)
        {
            if (Convert.ToBoolean(dataTable.Rows[0].ItemArray[1]))
                textResult.Text = "По паспорту «" + passportTextbox.Text + "» доступ к бюллетеню на дистанционном электронном голосовании ПРЕДОСТАВЛЕН";
            else
                textResult.Text = "По паспорту «" + passportTextbox.Text + "» доступ к бюллетеню на дистанционном электронном голосовании НЕ ПРЕДОСТАВЛЯЛСЯ";
        }
        else
            textResult.Text = "Паспорт «" + passportTextbox.Text + "» в списке участников дистанционного голосования НЕ НАЙДЕН";
    }
}
