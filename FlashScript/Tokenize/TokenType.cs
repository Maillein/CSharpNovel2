namespace FlashScript.Tokenize
{
    public enum TokenType
    {
        // 不正なトークン, 終端
        ILLEGAL,
        EOF,
        // 識別子、整数リテラル
        IDENT,
        INT,
        // 演算子
        ASSIGN,    // =
        PLUS,      // +    
        MINUS,     // -
        ASTERISK,  // *
        SLASH,     // /
        BANG,      // !
        LT,        // <
        LE,        // <=
        GT,        // >
        GE,        // >=
        EQ,        // ==
        NOT_EQ,    // !=
        // デリミタ
        COMMA,
        SEMICOLON,
        // 括弧(){}[]
        LPAREN,
        RPAREN,
        LBRACE,
        RBRACE,
        LSBRACE,
        RSBRACE,
        // タグ
        FUNCTION,
        LET,
        IF,
        ELSE,
        ENDIF,
        TRUE,
        FALSE,
        RETURN,
        CM,    // Clear Message
        // その他必要になったら追加します。 
    }
}