
using UnityEngine;

public interface IColor
{
   Color Blue { get; set; }
   Color Pink { get; set; } 
   Color Yellow { get; set; }
   Color Purple { get; set; }

   void SetColor(Color color); 
   void Initialize();
}
