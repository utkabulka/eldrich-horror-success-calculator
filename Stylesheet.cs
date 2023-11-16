namespace EldrichHorrorSuccessCalculator
{
  public class Styles
  {
    /// <summary>
    /// This string will be written to 'styles.css', so the table doesn't look ugly.
    /// It should be manually updated with contents of 'styles.css' file in this repository.
    /// </summary>
    public const string StylesheetString = @"@import url('https://fonts.googleapis.com/css2?family=Roboto:wght@400;700&display=swap');
html {
  font-size: 24px;
  font-family: Roboto, sans-serif;
  margin: 0;
  padding: 0;
}

h1 {
  font-size: 32px;
  text-align: center;
  margin: 16px;
}

.table-corner {
  font-size: 16px;
  font-weight: normal;
  margin: 4px 0 4px 0;
  padding: 0;
  rotate: 25deg;
}

hr {
  border-top: 1px solid #e8e8e8;
  margin: 0;
  padding: 0 8px 0 8px;
}

.table-block {
  display: flex;
  flex-direction: column;
  align-items: center;
  margin-bottom: 24px;
}

th,
.cubes-column {
  font-weight: bold;
}

th,
td {
  border: 1px solid #c8c8c8;
  padding: 8px 16px 8px 16px;
  min-width: 60px;
  text-align: center;
}

td.severity-5,
td.severity-6,
td.severity-7,
td.severity-8,
td.severity-9 {
  color: #e8e8e8;
}

td.severity-0 {
  background-color: #54de48;
}
td.severity-1 {
  background-color: #66de48;
}
td.severity-2 {
  background-color: #b4f641;
}
td.severity-3 {
  background-color: #eed43e;
}
td.severity-4 {
  background-color: #eeab3e;
}
td.severity-5 {
  background-color: #ee6a3e;
}
td.severity-6 {
  background-color: #ee583e;
}
td.severity-7 {
  background-color: #cc382e;
}
td.severity-8 {
  background-color: #b93e30;
}
td.severity-9 {
  background-color: #6e0000;
}";
  }
}
