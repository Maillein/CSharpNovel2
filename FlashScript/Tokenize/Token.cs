namespace FlashScript.Tokenize
{
    public class Token
    {
        public TokenType Type { get; set; }
        public string Literal { get; set; }
        
        public Token(TokenType type, string literal)
        {
            Type = type;
            Literal = literal;
        }
    }
}