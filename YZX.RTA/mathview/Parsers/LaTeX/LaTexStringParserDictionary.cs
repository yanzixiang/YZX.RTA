namespace mathview.Parsers.LaTeX
{
  using System.Collections.Generic;
  using mathview.Expressions;

  /// <summary>
  /// A static dictionary class for various interpretation functions.
  /// </summary>
  public static class LaTexStringParserDictionary
  {
    #region Operators
    private static object operatorsCreationLock = new object();
    private static Dictionary<string, ExpressionBase> operators = null;
    /// <summary>
    /// Gets a dictionary of operators.
    /// </summary>
    public static Dictionary<string, ExpressionBase> Operators
    {
      get
      {
        if (operators == null)
        {
          lock (operatorsCreationLock)
          {
            if (operators == null)
            {
              operators = new Dictionary<string, ExpressionBase>();

              // Dots, dots, dots.
              operators.Add("dots", new Operator(0x2026));
              operators.Add("dotso", new Operator(0x2026));
              operators.Add("dotsc", new Operator(0x2026));
              operators.Add("ldots", new Operator(0x2026));
              operators.Add("vdots", new Operator(0x22EE));
              operators.Add("cdots", new Operator(0x22EF));
              operators.Add("dotsb", new Operator(0x22EF));
              operators.Add("dotsm", new Operator(0x22EF));
              operators.Add("dotsi", new Operator(0x22EF));
              operators.Add("ddots", new Operator(0x22F1));

              // Relations.
              operators.Add("leq", new Operator(0x2264));
              operators.Add("ll", new Operator(0x226A));
              operators.Add("subseteq", new Operator(0x2286));
              operators.Add("nsubseteq", new Operator(0x2288));
              operators.Add("sqsubset", new Operator(0x228F));
              operators.Add("sqsubseteq", new Operator(0x2291));
              operators.Add("prec", new Operator(0x227A));
              operators.Add("preceq", new Operator(0x227C));
              operators.Add("geg", new Operator(0x2265));
              operators.Add("gg", new Operator(0x226B));
              operators.Add("supseteq", new Operator(0x2287));
              operators.Add("nsupseteq", new Operator(0x2289));
              operators.Add("sqsupset", new Operator(0x2290));
              operators.Add("sqsupseteq", new Operator(0x2292));
              operators.Add("succ", new Operator(0x227B));
              operators.Add("succeq", new Operator(0x227D));
              operators.Add("doteq", new Operator(0x2250));
              operators.Add("equiv", new Operator(0x2261));
              operators.Add("approx", new Operator(0x2248));
              operators.Add("cong", new Operator(0x2245));
              operators.Add("simeq", new Operator(0x2243));
              operators.Add("sim", new Operator(0x223C));
              operators.Add("propto", new Operator(0x221D));
              operators.Add("neq", new Operator(0x2260));
              operators.Add("parallel", new Operator(0x2225));
              operators.Add("asymp", new Operator(0x224D));
              operators.Add("vdash", new Operator(0x22A2));
              operators.Add("smile", new Operator(0x2323));
              operators.Add("models", new Operator(0x22A8));
              operators.Add("perp", new Operator(0x22A5));
              operators.Add("nparallel", new Operator(0x2226));
              operators.Add("bowtie", new Operator(0x22C8));
              operators.Add("dashv", new Operator(0x22A3));
              operators.Add("frown", new Operator(0x2322));
              operators.Add("mid", new Operator("|"));

              // Sets and Logic.
              operators.Add("forall", new Operator(0x2200));
              operators.Add("complement", new Operator(0x2201));
              operators.Add("partial", new Operator(0x1D715));
              operators.Add("exists", new Operator(0x2203));
              operators.Add("nexists", new Operator(0x2204));
              operators.Add("emptyset", new Operator(0x2205));
              operators.Add("varnothing", new Operator(0x2205));
              operators.Add("nabla", new Operator(0x2207));
              operators.Add("in", new Operator(0x2208));
              operators.Add("notin", new Operator(0x2209));
              operators.Add("ni", new Operator(0x220B));
              operators.Add("neg", new Operator(0x00AC));
              operators.Add("subset", new Operator(0x2282));
              operators.Add("supset", new Operator(0x2283));
              operators.Add("land", new Operator(0x2227));
              operators.Add("lor", new Operator(0x2228));
              operators.Add("rightarrow", new Operator(0x2192));
              operators.Add("to", new Operator(0x2192));
              operators.Add("leftarrow", new Operator(0x2190));
              operators.Add("gets", new Operator(0x2190));
              operators.Add("mapsto", new Operator(0x21A6));
              operators.Add("implies", new Operator(0x27F9));
              operators.Add("Rightarrow", new Operator(0x21D2));
              operators.Add("leftrightarrow", new Operator(0x2194));
              operators.Add("iff", new Operator(0x27FA));
              operators.Add("Leftrightarrow", new Operator(0x21D4));
              operators.Add("top", new Operator(0x22A4));
              operators.Add("bot", new Operator(0x22A5));

              // Binary Operators.
              operators.Add("pm", new Operator(0x00B1));
              operators.Add("mp", new Operator(0x2213));
              operators.Add("times", new Operator(0x00D7));
              operators.Add("div", new Operator(0x00F7));
              operators.Add("ast", new Operator(0x2217));
              operators.Add("star", new Operator(0x22C6));
              operators.Add("dagger", new Operator(0x2020));
              operators.Add("ddagger", new Operator(0x2021));
              operators.Add("cap", new Operator(0x22C2));
              operators.Add("cup", new Operator(0x22C3));
              operators.Add("uplus", new Operator(0x228E));
              operators.Add("sqcap", new Operator(0x2293));
              operators.Add("sqcup", new Operator(0x2294));
              operators.Add("vee", new Operator(0x2227));
              operators.Add("wedge", new Operator(0x2228));
              operators.Add("cdot", new Operator(0x22C5));
              operators.Add("diamond", new Operator(0x22C4));
              operators.Add("bigtriangleup", new Operator(0x0394));
              operators.Add("bigtriangledown", new Operator(0x2207));
              operators.Add("triangleleft", new Operator(0x22B2));
              operators.Add("triangleright", new Operator(0x22B3));
              operators.Add("bigcirc", new Operator(0x25CB));
              operators.Add("bullet", new Operator(0x2022));
              operators.Add("wr", new Operator(0x2240));
              operators.Add("oplus", new Operator(0x2295));
              operators.Add("ominus", new Operator(0x2296));
              operators.Add("otimes", new Operator(0x2297));
              operators.Add("oslash", new Operator(0x2298));
              operators.Add("odot", new Operator(0x2299));
              operators.Add("circ", new Operator(0x2218));
              operators.Add("setminus", new Operator(0x2216));
              operators.Add("amalg", new Operator(0x2A3F));

              // Delimiters.
              operators.Add("|", new Operator(0x2016));
              operators.Add("uparrow", new Operator(0x2191));
              operators.Add("downarrow", new Operator(0x2193));
              operators.Add("Uparrow", new Operator(0x21D1));
              operators.Add("Downarrow", new Operator(0x21D3));
              operators.Add("langle", new Operator(0x27E8));
              operators.Add("rangle", new Operator(0x27E9));
              operators.Add("backslash", new Operator("\\"));
              operators.Add("lceil", new Operator(0x2308));
              operators.Add("rceil", new Operator(0x2309));
              operators.Add("lfloor", new Operator(0x230A));
              operators.Add("rfloor", new Operator(0x230B));

              // Others.
              operators.Add("infty", new Operator(0x221E));
              operators.Add("box", new Operator(0x25A1));
              operators.Add("Re", new Operator(0x211C));
              operators.Add("Im", new Operator(0x2111));
              operators.Add("imath", new Operator(0x03B9)); // TODO: Unknown char!
              operators.Add("jmath", new Operator("j")); // TODO: Unknown char!
              operators.Add("ell", new Operator(0x2113));
              operators.Add("hbar", new Operator(0x210F));
              operators.Add("eth", new Operator(0x00F0));
              operators.Add("wp", new Operator(0x2118));
              operators.Add("aleph", new Operator(0x2135));
              operators.Add("beth", new Operator(0x2136));
              operators.Add("gimel", new Operator(0x2137));
            }
          }
        }

        return operators;
      }
    }
    #endregion

    #region Greek Letters
    private static object greekLettersCreationLock = new object();
    private static Dictionary<string, ExpressionBase> greekLetters = null;
    /// <summary>
    /// Gets a dictionary of greek symbols.
    /// </summary>
    public static Dictionary<string, ExpressionBase> GreekLetters
    {
      get
      {
        if (greekLetters == null)
        {
          lock (greekLettersCreationLock)
          {
            if (greekLetters == null)
            {
              greekLetters = new Dictionary<string, ExpressionBase>();
              // Small letters.
              greekLetters.Add("alpha", new Variable(0x03B1));
              greekLetters.Add("beta", new Variable(0x03B2));
              greekLetters.Add("gamma", new Variable(0x03B3));
              greekLetters.Add("delta", new Variable(0x03B4));
              greekLetters.Add("varepsilon", new Variable(0x03B5));
              greekLetters.Add("zeta", new Variable(0x03B6));
              greekLetters.Add("eta", new Variable(0x03B7));
              greekLetters.Add("theta", new Variable(0x03B8));
              greekLetters.Add("iota", new Variable(0x03B9));
              greekLetters.Add("kappa", new Variable(0x03BA));
              greekLetters.Add("lambda", new Variable(0x03BB));
              greekLetters.Add("mu", new Variable(0x03BC));
              greekLetters.Add("nu", new Variable(0x03BD));
              greekLetters.Add("xi", new Variable(0x03BE));
              greekLetters.Add("omicron", new Variable(0x03BF));
              greekLetters.Add("pi", new Variable(0x03C0));
              greekLetters.Add("rho", new Variable(0x03C1));
              greekLetters.Add("sigma", new Variable(0x03C3));
              greekLetters.Add("tau", new Variable(0x03C4));
              greekLetters.Add("upsilon", new Variable(0x03C5));
              greekLetters.Add("phi", new Variable(0x03C6));
              greekLetters.Add("chi", new Variable(0x03C7));
              greekLetters.Add("psi", new Variable(0x03C8));
              greekLetters.Add("omega", new Variable(0x03C9));
              // Small letter variants.
              greekLetters.Add("varsigma", new Variable(0x03C2));
              greekLetters.Add("epsilon", new Variable(0x03F5, 0x1D716));
              greekLetters.Add("vartheta", new Variable(0x03D1, 0x1D717));
              greekLetters.Add("varkappa", new Variable(0x3F0, 0x1D718));
              greekLetters.Add("varphi", new Variable(0x03D5, 0x1D719));
              greekLetters.Add("varrho", new Variable(0x03F1, 0x1D71A));
              greekLetters.Add("varpi", new Variable(0x03D6, 0x1D71B));
              // Big letters.
              greekLetters.Add("Alpha", new Variable(0x0391));
              greekLetters.Add("Beta", new Variable(0x0392));
              greekLetters.Add("Gamma", new Variable(0x0393));
              greekLetters.Add("Delta", new Variable(0x0394));
              greekLetters.Add("Epsilon", new Variable(0x0395));
              greekLetters.Add("Zeta", new Variable(0x0396));
              greekLetters.Add("Eta", new Variable(0x0397));
              greekLetters.Add("Theta", new Variable(0x0398));
              greekLetters.Add("Iota", new Variable(0x0399));
              greekLetters.Add("Kappa", new Variable(0x039A));
              greekLetters.Add("Lambda", new Variable(0x039B));
              greekLetters.Add("Mu", new Variable(0x039C));
              greekLetters.Add("Nu", new Variable(0x039D));
              greekLetters.Add("Xi", new Variable(0x039E));
              greekLetters.Add("Omicron", new Variable(0x039F));
              greekLetters.Add("Pi", new Variable(0x03A0));
              greekLetters.Add("Rho", new Variable(0x03A1));
              greekLetters.Add("Sigma", new Variable(0x03A3));
              greekLetters.Add("Tau", new Variable(0x03A4));
              greekLetters.Add("Upsilon", new Variable(0x03A5));
              greekLetters.Add("Phi", new Variable(0x03A6));
              greekLetters.Add("Chi", new Variable(0x03A7));
              greekLetters.Add("Psi", new Variable(0x03A8));
              greekLetters.Add("Omega", new Variable(0x03A9));
            }
          }
        }

        return greekLetters;
      }
    }
    #endregion

    /// <summary>
    /// Converts a string to a corresponding expression.
    /// </summary>
    /// <param name="command">The command string.</param>
    /// <returns>The corresponding expression or an <see cref="Atom"/> instead.</returns>
    public static ExpressionBase CommandToExpression(string command)
    {
      ExpressionBase ret = null;

      // Try operators.
      if (Operators.TryGetValue(command, out ret))
        return ret;

      // Try greek letters.
      if (GreekLetters.TryGetValue(command, out ret))
        return ret;

      // Try to find a suitable expression.
      switch (command)
      {
        case "{": return new Atom("{");
        case "}": return new Atom("}");

        case ",": return new Whitespace(3.0 / 18.0);
        case ":": return new Whitespace(4.0 / 18.0);
        case ";": return new Whitespace(5.0 / 18.0);
        case "!": return new Whitespace(-3.0 / 18.0);
        case "quad": return new Whitespace(1.0);

        case "frac": return new Fraction(FractionStyle.Default);
        case "tfrac": return new Fraction(FractionStyle.Default, FractionSize.Text);
        case "dfrac": return new Fraction(FractionStyle.Default, FractionSize.Display);
        case "sfrac": return new Fraction(FractionStyle.Slanted, FractionSize.Text);
        case "binom": return new Fraction(FractionStyle.Binomial);
        case "tbinom": return new Fraction(FractionStyle.Binomial, FractionSize.Text);
        case "dbinom": return new Fraction(FractionStyle.Binomial, FractionSize.Display);
      }

      // Fallback to a simple atom with the command syntax as text.
      return new Atom('\\' + command, true);
    }
  }
}
