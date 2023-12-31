
public abstract class Abstract_Figure: Expression {

      public List<Expression> Components;
      public Abstract_Figure( List<Expression> components) { Components= components; }

    }


    public class Abstract_Circle: Abstract_Figure { 

     public Abstract_Circle( List<Expression> components) : base( components) { Console.WriteLine("Creating_Circle"); }  

     public override Bool_Object Evaluate( Context context ) {
      
      if( Components.Count!=2) return new Bool_Object(false, null);
      var list= Utils.Filter( context, Components[0], Components[1] );
      if(list==null) Console.WriteLine( "list_nula");
      if( !(list[0] is Point) || !(list[1] is Measure)) Console.WriteLine( "Incorrect_Paramters");
      if(list==null || !(list[0] is Point) || !(list[1] is Measure) ) return new Bool_Object(false, null);
  
       return new Bool_Object(true, new Circle( (Point)list[0], (Measure)list[1]) );

     }

    }



    public class Abstract_Line: Abstract_Figure {

      public string Especification;

      public Abstract_Line( List<Expression> components, string especification) : base( components) {

        Especification= especification;
      
      }

      public override Bool_Object Evaluate( Context context) {

       if( Components.Count!=2 ) return new Bool_Object( false, null);
       var list= Utils.Filter<Point>( context, new Point(), true, Components[0], Components[1] );
       if( list==null) return new Bool_Object( false, null);
       return new Bool_Object( true, Utils.Make_Line( (Point)list[0], (Point)list[1], Especification) );

      }  

    }


    public class Abstract_Point: Abstract_Figure {
    
    public Abstract_Point( List<Expression> components) : base( components) { }

    public override Bool_Object Evaluate( Context context) {

       if( Components.Count!=2 ) return new Bool_Object( false, null);
       double aux= 5;
       var list= Utils.Filter<double>( context, aux, true, Components[0], Components[1] );
       
       if( list==null) return new Bool_Object( false, null);
       
       return new Bool_Object( true, new Point( (double)list[0], (double)list[1] ) );

      }  
 
    }


     public class Abstract_Measure: Abstract_Figure {

      public Abstract_Measure( List<Expression> components) : base( components) { Console.WriteLine("Creating_measure"); }

      public override Bool_Object Evaluate( Context context ) { 

       if( Components.Count!=2 ) return new Bool_Object( false, null);
       var list= Utils.Filter<Point>( context, new Point(), true, Components[0], Components[1] );
       if( list==null) return new Bool_Object( false, null);
       return new Bool_Object( true, new Measure( (Point)list[0], (Point)list[1] ) );

      }

     }



     public class Abstract_Arc: Abstract_Figure {
    
     public Abstract_Arc( List<Expression> components) : base( components) {}

     public override Bool_Object Evaluate( Context context) {

       if( Components.Count!=4 ) return new Bool_Object( false, null);
       var list= Utils.Filter<Point>( context, new Point(), true, Components[0], Components[1], Components[2] );
       if( list==null) return new Bool_Object( false, null);
       var pair= Components[3].Evaluate( context);
       if( !pair.Bool || !(pair.Object is Measure) ) new Bool_Object( false, null);

       return new Bool_Object( true, new Arc( (Point)list[0], (Point)list[1], (Point)list[2], (Measure)pair.Object ) );

      }  
 
    }

    public class Abstract_Print : No_Computable  {

     public Expression Expr;
     public string Coment { get; set; }

     public Abstract_Print( Expression expr ) { Expr= expr; Console.WriteLine("Creating_Abstract_Print"); }
     public Abstract_Print( Expression expr, String coment ) { 
      
      Expr= expr; 
      Coment= coment.Value;

     }

     public override Bool_Object Evaluate( Context context ) {

       var obj= Expr.Evaluate( context).Object;
       if( obj== null ) return new Bool_Object( false, null);
       
      if( Coment== null)  context.Add_Figure( new Printer( obj.ToString()) );
      else context.Add_Figure( new Printer( obj.ToString(), Coment ) );

      return new Bool_Object( true, null);
       
     }


    }



    