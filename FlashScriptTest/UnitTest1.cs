using System.Collections.Generic;
using System.IO;
using System.Linq;
using FlashScript;
using FlashScript;
using NUnit.Framework;
using Sprache;

namespace FlashScriptTest
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void TestParseTag1()
        {
            var src = "[cm]";
            var result = ParseScript.ParseTag.Parse(src);
            var tag = result as Tag;
            Assert.NotNull(tag, "resultがTagではありません。");
            Assert.AreEqual(src, tag.ToString(), "正しくパースできませんでした。");
        }

        [Test]
        public void TestParseTag2()
        {
            var tagSource = "[ns name:\"Maillein\", age:21, size:170,]";
            var tagParsed = ParseScript.ParseTag.Parse(tagSource);
            var tag = tagParsed as Tag;
            Assert.NotNull(tag, "tagParsedがTagではありません。");
            Assert.AreEqual(tagSource, tag.ToString(), "タグをパースできませんでした。");
            Assert.AreEqual(new MString("Maillein").ToString(), tag.Args["name"].ToString(), "nameが正しくありません。");
            Assert.AreEqual(new MInt(21).ToString(), tag.Args["age"].ToString(), "ageが正しくありません。");
            Assert.AreEqual(new MInt(170).ToString(), tag.Args["size"].ToString(), "sizeが正しくありません。");
        }

        [Test]
        public void TestParseLines()
        {
            var linesSource = "「吾輩は猫である。」";
            var linesParsed = ParseScript.ParseLines.Parse(linesSource);
            var lines = linesParsed as Lines;
            Assert.NotNull(lines, "linesParsedがLinesではありませんでした。");
            Assert.AreEqual(linesSource, lines.ToString(), "セリフをパースできませんでした。");
            Assert.AreEqual("吾輩は猫である。", lines.Value, "台詞の内容が正しくありません。");
        }

        [Test]
        public void TestParseLabel()
        {
            var labelSource = "*start";
            var labelParsed = ParseScript.ParseLabel.Parse(labelSource);
            var label = labelParsed as Label;
            Assert.NotNull(label, "labelParsedがLabelではありません。");
            Assert.AreEqual(labelSource, label.ToString(), "ラベルをパースできませんでした。");
            Assert.AreEqual("start", label.Value, "ラベルの内容が正しくありません。");
        }

        [Test]
        public void TestParseVariable()
        {
            var valueSource = "$abc";
            var valueParsed = ParseScript.ParseVariable.Parse(valueSource);
            var value = valueParsed as Variable;
            Assert.NotNull(value, "valueParsedがVariableではありませんでした。");
            Assert.AreEqual(valueSource, value.ToString(), "変数を正しくパースできません。");
            Assert.AreEqual("abc", value.Name, "変数の名前が正しくありません。");
        }

        [Test]
        public void TestParseLabelOrLinesOrTag1()
        {
            var src = "「今日もいい天気。」";
            var result = ParseScript.ParseMain.Parse(src);
            var lines = result as Lines;
            Assert.NotNull(lines, "Linesではありません。");
        }

        [Test]
        public void TestParseLabelOrLinesOrTag2()
        {
            var src = "*label";
            var result = ParseScript.ParseMain.Parse(src);
            var label = result as Label;
            Assert.NotNull(label, "Labelではありません。");
        }

        [Test]
        public void TestParseLabelOrLinesOrTag3()
        {
            var src = "[sn name:\"Ryuichi\"]";
            var result = ParseScript.ParseMain.Parse(src);
            var label = result as Tag;
            Assert.NotNull(label, "Tagではありません。");
        }

        [Test]
        public void TestParseProgram1()
        {
            var src = @"*start
[sn name:""少女A""]
「おっはよー」
[cm]
[sn name:""龍一""]
「おはよー」[l][r]";
            var result = ParseScript.ParseProgram.Parse(src);
            var tests = new List<Expr>
            {
                new Label("start"),
                new Tag("sn", new[] {new TagArg("name", new MString("少女A")),}),
                new Lines("おっはよー"),
                new Tag("cm", null),
                new Tag("sn", new[] {new TagArg("name", new MString("龍一")),}),
                new Lines("おはよー"),
                new Tag("l", null),
                new Tag("r", null),
            };
            Assert.AreEqual(tests.Count, result.exprs.Count(), "Exprの数が違います。");
            for (var i = 0; i < tests.Count; i++)
            {
                Assert.AreEqual(tests[i].ToString(), result.exprs[i].ToString());
            }
        }
    }
}