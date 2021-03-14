using System.Collections.Generic;
using System.Text;
using Sprache;

namespace FlashScript
{
    public enum Type
    {
        M_STRING,
        M_INT,
        M_DECIMAL,
        LABEL,
        LINES,
        VARIABLE,
        TAG,
        TAG_ARG,
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
        public MString(string value):base(Type.M_STRING) => Value = value;
        public override string ToString() => $"\"{Value}\"";
    }

    public class MInt : Literal
    {
        public long Value { get; }
        public MInt(long value):base(Type.M_INT) => Value = value;
        public override string ToString() => Value.ToString();
    }

    public class MDecimal : Literal
    {
        public double Value { get; }
        public MDecimal(double value):base(Type.M_DECIMAL) => Value = value;
        public override string ToString() => Value.ToString();
    }

    public class Label: Literal 
    {
        public string Value { get; }
        public Label(string value):base(Type.LABEL) => Value = value;
        public override string ToString() => $"*{Value}";
    }

    public class Lines : Literal
    {
        public string Value { get; }
        public Lines(string value):base(Type.LINES) => Value = value;
        public override string ToString() => $"「{Value}」";
    }

    public class Variable : Expr 
    {
        public string Name { get; }
        public Variable(string name):base(0, Type.VARIABLE) => Name = name;
        public override string ToString() => "$" + Name;
    }
    
    public class Tag : Expr
    {
        public string Name { get; }
        public Dictionary<string, Expr> Args { get; } = new Dictionary<string, Expr>();

        public Tag(string name, IEnumerable<Expr> args) : base(0, Type.TAG)
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

        public TagArg(string key, Expr value) : base(0, Type.TAG_ARG) => (Key, Value) = (key, value);
    }

    public class Script
    {
        public List<Expr> exprs;
        public Script(IEnumerable<Expr> e) => exprs = e as List<Expr>;
    }
}