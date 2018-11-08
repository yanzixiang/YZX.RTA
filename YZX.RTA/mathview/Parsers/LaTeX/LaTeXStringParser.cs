namespace mathview.Parsers.LaTeX
{
  using mathview.Expressions;
  using System.Collections.Generic;
  using System.IO;
  using System.Linq;
  using System.Text;
  using System;

  /// <summary>
  /// A string parser.
  /// </summary>
  public class LaTeXStringParser : LaTeXParserBase
  {
    #region BlockStackEntry class
    /// <summary>
    /// A class for entries of the block stack.
    /// </summary>
    private class BlockStackEntry
    {
      #region Variables & Properties
      /// <summary>
      /// A stack of commands.
      /// </summary>
      private Stack<CompositeExpression> CommandExprStack = new Stack<CompositeExpression>();

      /// <summary>
      /// The current command expression.
      /// </summary>
      private CompositeExpression CommandExpr = null;

      /// <summary>
      /// The current sub- and superscript expression.
      /// </summary>
      private SubAndSuperscript SubAndSuperscriptExpr = null;

      /// <summary>
      /// The current type (used for sub- and superscript).
      /// </summary>
      private CharType SubAndSuperscriptType = CharType.Unknown;

      /// <summary>
      /// The current expression.
      /// </summary>
      private ExpressionBase CurrentExpr = null;

      /// <summary>
      /// The result.
      /// </summary>
      public ExpressionBase Result = null;

      /// <summary>
      /// The fraction stack counter.
      /// </summary>
      public int FractionStackCounter = 0;

      /// <summary>
      /// Get a flag that controls to use only one character per expression.
      /// </summary>
      public bool UseOneCharPerExpression
      {
        get
        {
          if (CommandExpr != null)
            return CommandExpr.UseOneCharPerExpression;
          else if (SubAndSuperscriptExpr != null)
          {
            return !((SubAndSuperscriptType == CharType.Subscript && SubAndSuperscriptExpr.SubScript != null) ||
                (SubAndSuperscriptType == CharType.Superscript && SubAndSuperscriptExpr.SuperScript != null));
          }
          else
            return false;
        }
      }

      /// <summary>
      /// Get the required expression count, if available.
      /// </summary>
      public int RequiredExpressionCount
      {
        get { return CommandExpr != null ? ((CompositeExpression)CommandExpr).RequiredExpressionCount : 0; }
      }
      #endregion

      /// <summary>
      /// Set the new expression.
      /// </summary>
      /// <param name="exprStr">The new expression string.</param>
      public void SwitchCurrentExpression<T>(string exprStr) where T : ExpressionBase, ITextExpression, new()
      {
        var expr = new T() { Text = exprStr };
        SwitchCurrentExpression(expr);
      }

      /// <summary>
      /// Set the new expression.
      /// </summary>
      /// <param name="newExpr">The new expression.</param>
      public void SwitchCurrentExpression(ExpressionBase newExpr)
      {
        var updateResult = true;

        // Whitespace insertion control at operators.
        if (newExpr is Operator)
        {
          var op = (Operator)newExpr;
          // TODO @ Parser: Still errors in whitespace insertion...

          if (CurrentExpr == null || (CurrentExpr is Operator && ((Operator)CurrentExpr).HasRightWhitespace))
          {
            op.HasLeftWhitespace = false;
            op.HasRightWhitespace = false;
          }
        }

        // Control the behaviour of sub- and superscript.
        if (SubAndSuperscriptExpr != null)
        {
          if (SubAndSuperscriptType == CharType.Subscript)
          {
            if (SubAndSuperscriptExpr.SubScript == null)
            {
              SubAndSuperscriptExpr.SubScript = newExpr;
              return;
            }
            else
            {
              SubAndSuperscriptExpr = null;
              SubAndSuperscriptType = CharType.Unknown;
            }
          }
          else
          {
            if (SubAndSuperscriptExpr.SuperScript == null)
            {
              SubAndSuperscriptExpr.SuperScript = newExpr;
              return;
            }
            else
            {
              SubAndSuperscriptExpr = null;
              SubAndSuperscriptType = CharType.Unknown;
            }
          }
        }

        // Assign new expression to current expression.
        CurrentExpr = newExpr;

        // Handle command expressions. Stop adding expressions after the required count was reached.
        if (CommandExpr != null)
        {
          updateResult = false;
          CommandExpr.AddEpression(CurrentExpr);
          CurrentExpr = CommandExpr;

          if (CommandExpr.RequiredExpressionCount == 0)
          {
            // Handle fractions before they are finished.
            if (CommandExpr is Fraction)
              FractionStackCounter--;

            // Set the previous command expression or null.
            if (CommandExprStack.Count > 0)
              CommandExpr = CommandExprStack.Pop();
            else
              CommandExpr = null;
          }
        }

        // Check, whether this expression is a composite expression and therefore needs input blocks.
        if (CurrentExpr is CompositeExpression && (CurrentExpr != CommandExpr) && ((CompositeExpression)CurrentExpr).RequiredExpressionCount > 0)
        {
          if (CommandExpr != null)
            CommandExprStack.Push(CommandExpr);
          CommandExpr = (CompositeExpression)CurrentExpr;

          // Handle the fraction stack.
          if (newExpr is Fraction)
          {
            FractionStackCounter++;
            ((Fraction)newExpr).IsSubFraction = FractionStackCounter > 1;
          }
        }

        // Break here, if needed.
        if (updateResult)
        {
          // Set the new result:
          // - If the last result was null, copy the new expression.
          // - Otherwise, try to cast to a block or create a new one.
          if (Result == null)
            Result = CurrentExpr;
          else
          {
            var block = (Result is Block) ? (Block)Result : new Block() { Expressions = { Result } };
            block.Expressions.Add(CurrentExpr);
            Result = block;
          }
        }
      }

      /// <summary>
      /// Creates a new sub-/superscript object.
      /// </summary>
      /// <param name="type">The type.</param>
      public void CreateSubAndSuperscript(CharType type)
      {
        if (SubAndSuperscriptExpr != null && (SubAndSuperscriptType == type || (type == CharType.Subscript && SubAndSuperscriptExpr.SubScript == null) || (type == CharType.Superscript && SubAndSuperscriptExpr.SuperScript == null)))
        {
          // Append the expression.
        }
        else
        {
          if (SubAndSuperscriptExpr != null)
            CurrentExpr = null;

          var script = new SubAndSuperscript();
          script.Expression = CurrentExpr;

          SubAndSuperscriptExpr = script;
          CurrentExpr = script;

          // Update the result.
          if (Result == null || !(Result is Block))
            Result = CurrentExpr;
          else
          {
            var block = (Block)Result;
            block.Expressions[block.Expressions.Count - 1] = CurrentExpr;
          }
        }

        SubAndSuperscriptType = type;
      }
    }
    #endregion

    /// <summary>
    /// Parse a stream.
    /// </summary>
    /// <param name="stream">The stream.</param>
    /// <param name="encoding">The encoding of the stream.</param>
    /// <returns>An expression tree representing the content of the stream.</returns>
    public override ExpressionBase Parse(Stream stream, Encoding encoding)
    {
      var reader = new StreamReader(stream, encoding);
      var root = new Block() { IsVertical = true };

      while (!reader.EndOfStream)
      {
        var line = reader.ReadLine();
        var expr = ParseLine(line);
        if (expr != null)
          root.Expressions.Add(expr);
      }

      return root;
    }

    private enum CharType
    {
      Unknown,
      EscapedCommand,
      Digit,
      Letter,
      Operator,
      Subscript,
      Superscript,
    }

    private ExpressionBase ParseLine(string line)
    {
      // TODO @ ParseLine: Brackets
      // TODO @ ParseLine: Integrals
      // TODO @ ParseLine: Roots
      // TODO @ ParseLine: Sums & Products
      // TODO @ ParseLine: Handle °C and °F (low priority).

      var isCommand = false;
      var exprText = "";
      var blockStack = new Stack<BlockStackEntry>();
      var currentEntry = new BlockStackEntry();
      var lastType = CharType.Unknown;
      var type = CharType.Unknown;

      // This method adds a new expression (if existing) and clears the expression text.
      // To be used only in this context.
      Action AddAndClear = () =>
      {
        // Check, if the expression text is not empty.
        if (!string.IsNullOrWhiteSpace(exprText))
        {
          // Create the new expression.
          switch (lastType)
          {
            case CharType.Letter:
              currentEntry.SwitchCurrentExpression<Variable>(exprText);
              break;
            case CharType.Digit:
              currentEntry.SwitchCurrentExpression<Atom>(exprText);
              break;
          }

          // Clear the expression text.
          exprText = "";
          lastType = CharType.Unknown;
        }
      };

      // Trim leading and trailing whitespace characters and get the length.
      line.Trim();
      var max = line.Length;

      for (var i = 0; i < max; i++)
      {
        // Replace special characters.
        var c = line[i];

        // Filter whitespace characters.
        if (char.IsWhiteSpace(c))
          continue;

        // Create control flags.
        var isLastChar = (i == max - 1);

        if (c == '\\')
          type = CharType.EscapedCommand;
        else if (c == '_')
          type = CharType.Subscript;
        else if (c == '^')
          type = CharType.Superscript;
        else if (char.IsDigit(c) || (c == '.') || (c == ','))
          type = CharType.Digit;
        else if (char.IsLetter(c) || c == '°')
          type = CharType.Letter;
        else
          type = CharType.Operator;

        // State transistions.
        if (isCommand)
        {
          // Check, if the end of the command is reached.
          // Otherwise, add the character to the command.
          if (c == '{' || c == '}' || type == CharType.EscapedCommand || type != CharType.Letter || isLastChar)
          {
            // Filter out escaped brackets.
            var isEscapedBracket = (exprText.Length == 0);
            var isLastLetter = isLastChar && (type == CharType.Letter);

            // Append the current character, if allowed.
            if (isEscapedBracket || isLastLetter)
              exprText += c;

            // Interpret the command.
            isCommand = (type == CharType.EscapedCommand);
            var expr = LaTexStringParserDictionary.CommandToExpression(exprText);
            currentEntry.SwitchCurrentExpression(expr);

            // Clear the expression text.
            exprText = "";
            lastType = CharType.Unknown;

            // Continue in case of whitespace and escaped brackets or commands.
            if (isEscapedBracket || isCommand || isLastLetter)
              continue;
          }
          else
            exprText += c;
        }

        if (!isCommand)
        {
          // Look for an escaped command.
          if (type == CharType.EscapedCommand)
          {
            // Start the new command.
            AddAndClear();
            isCommand = true;
          }
          else
          {
            // Look for blocks.
            if (c == '{')
            {
              AddAndClear();

              // This character denotes a beginning block.
              blockStack.Push(currentEntry);
              currentEntry = new BlockStackEntry() { FractionStackCounter = currentEntry.FractionStackCounter };
            }
            else if (c == '}')
            {
              AddAndClear();

              // Pop back the previous block in the hierarchy and add the current result to this block.
              if (blockStack.Count > 0)
              {
                var result = currentEntry.Result;
                currentEntry = blockStack.Pop();
                currentEntry.SwitchCurrentExpression(result);
              }
            }
            else if (type == CharType.Operator)
            {
              // Create the operator.
              AddAndClear();
              currentEntry.SwitchCurrentExpression<Operator>("" + c);
            }
            else if ((type == CharType.Subscript || type == CharType.Superscript) && (currentEntry.RequiredExpressionCount == 0))
            {
              // We have a sub/superscript command and no open tasks.
              AddAndClear();
              currentEntry.CreateSubAndSuperscript(type);
            }
            else
            {
              // Check, if the type of the character changed.
              if (lastType != CharType.Unknown && lastType != type)
                AddAndClear();

              // Add a normal character.
              exprText += c;
              lastType = type;

              // Check, if only one character is allowed or the last character was reached.
              if (currentEntry.UseOneCharPerExpression || isLastChar)
                AddAndClear();
            }
          }
        }
      }

      return currentEntry.Result;
    }
  }
}
