using System.Collections.Generic;
using System.Text;
using Sprache;

namespace FlashScript
{
    public enum Type
    {
        MString,
        MInt,
        MDecimal,
        Label,
        Lines,
        Variable,
        Tag,
        TagArg,
    }
    public abstract class Expr
    {
        public int Priority { get; }
        public Type Type { get; }
        public Expr(int priority, Type type)
        {
            Priority = priority;
            Type = type;
        }

        
        public static Expr Identifier(string name) => new Variable(name);
        public static Expr Tag(string name, IEnumerable<Expr> args) => new Tag(name, args);
    }

    public abstract class Literal : Expr
    {
        public Literal(Type type): base(0, type){}
    }

    public class MString : Literal
    {
        public string Value { get; }
        public MString(string value):base(Type.MString) => Value = value;
        public override string ToString() => $"\"{Value}\"";
    }

    public class MInt : Literal
    {
        public long Value { get; }
        public MInt(long value):base(Type.MInt) => Value = value;
        public override string ToString() => Value.ToString();
    }

    public class MDecimal : Literal
    {
        public double Value { get; }
        public MDecimal(double value):base(Type.MDecimal) => Value = value;
        public override string ToString() => Value.ToString();
    }

    public class Label: Literal 
    {
        public string Value { get; }
        public Label(string value):base(Type.Label) => Value = value;
        public override string ToString() => $"*{Value}";
    }

    public class Lines : Literal
    {
        public string Value { get; }
        public Lines(string value):base(Type.Lines) => Value = value;
        public override string ToString() => $"「{Value}」";
    }

    public class Variable : Expr 
    {
        public string Name { get; }
        public Variable(string name):base(0, Type.Variable) => Name = name;
        public override string ToString() => "$" + Name;
    }
    
    public class Tag : Expr
    {
        public string Name { get; }
        public Dictionary<string, Expr> Args { get; } = new Dictionary<string, Expr>();

        public Tag(string name, IEnumerable<Expr> args) : base(0, Type.Tag)
        {
            Name = name;
            if (args == null)
            {
                Args = new Dictionary<string, Expr>();
                return;
            }
            foreach (var item in args)
                if (item is TagArg tagArg)
                    Args.Add(tagArg.Key, tagArg.Value);
        }

        public override string ToString()
        {
            var builder = new StringBuilder();
            builder.Append("[");
            builder.Append(Name);
            foreach (var (key, value) in Args)
            {
                builder.Append($" {key}:{value},");
            }

            builder.Append("]");
            return builder.ToString();
        }
    }

    public class TagArg : Expr
    {
        public string Key { get; }
        public Expr Value { get; }

        public TagArg(string key, Expr value) : base(0, Type.TagArg) => (Key, Value) = (key, value);
    }

    public class Script
    {
        public List<Expr> exprs;
        public Script(IEnumerable<Expr> e) => exprs = e as List<Expr>;
    }
}