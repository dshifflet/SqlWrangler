using System.Drawing;
using ScintillaNET;

namespace SqlWrangler.Services
{
    internal class ScintillaStyler
    {
        public void StyleElement(Scintilla element)
        {
            element.StyleResetDefault();
            element.Styles[Style.Default].Font = "Consolas";
            element.Styles[Style.Default].Size = 12;
            element.StyleClearAll();

            element.Styles[Style.Sql.Default].ForeColor = Color.Silver;
            element.Styles[Style.Sql.Comment].ForeColor = Color.FromArgb(0, 128, 0); // Green
            element.Styles[Style.Sql.CommentLine].ForeColor = Color.FromArgb(0, 128, 0); // Green
            element.Styles[Style.Sql.CommentLineDoc].ForeColor = Color.FromArgb(128, 128, 128); // Gray
            element.Styles[Style.Sql.Number].ForeColor = Color.Olive;
            element.Styles[Style.Sql.Word].ForeColor = Color.Blue;
            element.Styles[Style.Sql.Word2].ForeColor = Color.Red;
            element.Styles[Style.Sql.String].ForeColor = Color.FromArgb(163, 21, 21); // Red
            element.Styles[Style.Sql.Character].ForeColor = Color.FromArgb(163, 21, 21); // Red
            //element.Styles[Style.Sql.Verbatim].ForeColor = Color.FromArgb(163, 21, 21); // Red
            //element.Styles[Style.Sql.StringEol].BackColor = Color.Pink;
            element.Styles[Style.Sql.Operator].ForeColor = Color.Purple;
            //element.Styles[Style.Sql.Preprocessor].ForeColor = Color.Maroon;            
            element.Lexer = Lexer.Sql;
            element.SetKeywords(0, "select from where inner join outer left right on order by group update insert delete truncate");
            element.SetKeywords(1, "commit rollback");
        }
    }
}
