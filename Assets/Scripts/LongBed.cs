using UnityEngine;
public class LongBed : MonoBehaviour
{
   public GameObject Bed;
   public int width = 10;
   public int length = 2;
  
   void Start()
   {
       for (int y=0; y < length; ++y)
       {
           for (int x=0; x < width; ++x)
           {
               Instantiate(Bed, new Vector3(x,0,y) + this.transform.localPosition, Quaternion.identity);
           }
       }       
   }
}