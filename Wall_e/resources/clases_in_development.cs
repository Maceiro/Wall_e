
public class Arc: Figure {

  public Point center;
  public Point initial;
  public Point final;
  public Measure radio;

  public Arc( Point center, Point initial, Point final, Measure radio ) {

    this.center= center;
    this.initial= initial;
    this.final= final;
    this.radio= radio;

  }

  public override List<Point> Get_Intersection( Figure other) {

    var segment1= new Segment( center, initial);
    var segment2= new Segment( center, final);
    var circle= new Circle( center, radio);
    var result= new List<Point>();
    result.Aggregate( segment1.Get_Intersection( other));
    result.Aggregate( segment2.Get_Intersection( other));
    var list= circle.Get_Intersection( other);
    Filter( list, segment1.Get_Ecuation().V, segment2.Get_Ecuation.V, center);
    result.Aggregate( list);
    return result;

  }

  public override Ecuation Get_Ecuation() { return null; }

 }


 public static bool Validate_Program( string chain ) {
   
   if( Semantik_Analysis.AST== null ) Semantik_Analysis.AST= new Program_Node() ;
   if( Semantik_Analysis.Context== null ) {
    Semantik_Analysis.Context= new Context() ;
    Semantik_Analysis.Context.Introduce_Functions() ; 
   }

   string aux="";
   var tree= (Program_Node)Semantik_Analysis.AST ;

   for( int i= 0; i< chain.Length; i++) 
    if( chain[i]==';' ||  i== chain.Length-1  ) {
     
     aux= ( chain[i]==';' )? chain.Substring( 0, i ) : chain.Substring( 0, i+1 )  ; 
     //Console.WriteLine( aux) ;
     var t= Obtain_AST( aux ) ; 
     if( !t.Item2 ) return false ; 
     var sub_tree= t.Item1 ;
     Console.WriteLine("AST_Completed");
     
     var pair= sub_tree.Evaluate( Semantik_Analysis.Context);
     if( !pair.Bool) {

      Console.WriteLine("False");
      return false ;
     }
     if( i== chain.Length-1 && chain[i]!=';' ) {
     
      Print_in_Console( "Sintax Error :  You must finish the instruction with a ';' caracter") ;
       return false ;
     }
     
     if( pair.Object!= null ) Print_in_Console( pair.Object );
     else Console.WriteLine( "Objecto_null");
     tree.lines.Add( sub_tree ) ;
     return true ;

    }
    return true ;
    
  }