using System;
using System.Collections.Generic;
using Sprache;

namespace FlashScript
{
    public class ParseScript
    {
        public static Parser<Expr> ParseVariable =
            from start in Parse.Char('$')
            from space in Parse.WhiteSpace.Many()
            from name in Parse.Lower.Or(Parse.Upper).Or(Parse.Digit).Many().Text()
            select new Variable(name);

        public static Parser<Expr> ParseLabel =
            from start in Parse.Char('*')
            from space in Parse.WhiteSpace.Many()
            from value in Parse.Lower.Or(Parse.Upper).Or(Parse.Digit).Many().Text()
            select new Label(value);

        public static Parser<Expr> ParseMString =
            from start in Parse.Char('"')
            from content in Parse.CharExcept(new[] {'"'}).Many().Text()
            from close in Parse.Char('"')
            select new MString(content);

        public static Parser<Expr> ParseMInt =
            from value in Parse.Number
            select new MInt(Int64.Parse(value));

        public static Parser<Expr> ParseMDecimal =
            from value in Parse.Decimal
            select new MDecimal(Double.Parse(value));

        public static Parser<Expr> ParseLines =
            from open in Parse.Char('「')
            from content in Parse.CharExcept(new[] {'「', '」'}).Many().Text()
            from close in Parse.Char('」')
            select new Lines(content);

        public static Parser<string> ParseTagArgKey =
            Parse.LetterOrDigit.AtLeastOnce().Text().Token();

        public static Parser<Expr> ParseTagArg =
            from key in ParseTagArgKey
            from _ in Parse.Char(':')
            from value in ParseMInt.Or(ParseMDecimal).Or(ParseMString)
            select new TagArg(key, value);
        
        public static Parser<Expr> ParseTag =
            from open in Parse.Char('[')
            from name in Parse.LetterOrDigit.AtLeastOnce().Text()
            from args in Parse.Ref(() => ParseTagArg).DelimitedBy(Parse.Char(',').Token()).Optional()
            from comma in Parse.Char(',').Optional()
            from close in Parse.Char(']')
            select new Tag(name, args.GetOrDefault());

        public static Parser<Expr> ParseMain =
            from expr in ParseLabel.Or(ParseLines).Or(ParseTag)
            from _ in Parse.LineEnd.Optional()
            select expr;

        public static Parser<Script> ParseProgram =
            (from expr in ParseMain.Many()
                select new Script(expr)).End();
    }
}