using System;
using voiture;
using System.Threading;
using System.Collections;
using System.Linq;
namespace affichage{
    public class Affichage{

        public void header(){
            Console.WriteLine("|-------------|-------|-------|-------|--------|------|------------|");
            Console.WriteLine("| Période     |  S1   |  S2   |  S3   | status | tour | temps-tour |");
            Console.WriteLine("|------------------------------------------------------------------|");
        }

        public void body(int[][] classement){
            int[][] temp = CopyArray(classement);           
            Array.Sort(temp,  new Comparison<int[]>( 
                (x,y) => { return x[6] < y[6] ? -1 : (x[6] > y[6] ? 1 : 0); }
            ));
            foreach(int[] o in temp){
                    
                    Console.WriteLine(information(o));
            }
        }
        public String information(int[] o){
                      
            String s ="| Course      ";
            s += formatSecteur(o[2]);
            s += formatSecteur(o[3]);
            s += formatSecteur(o[4]);
            s += formatStatus(o[5]);
            s += formatTour(o[1]);
            s += formatTempsTour(o[6]);
            return s;
        }
        public bool affichage(int[][] classement){
            
            header();
            body(classement);
            return true;
        }

        public String formatSecteur(int nbr){
            String s = "";
            if(nbr<10){
                s += "| " + nbr+"     ";
            }else if(nbr<100){
                s += "| " + nbr+"    ";
            }else if(nbr<1000){
                s += "| " + nbr+"   ";
            }
            
            return s;
        }
        public String formatTempsTour(int nbr){
            String s = "";
            if(nbr<1000){
                s += "| " + nbr+"       ";
            }else if(nbr<100){
                s += "| " + nbr+"        ";
            }else if(nbr<10){
                s += "| " + nbr+"         ";
            }
            
            return s;
        }
        public String formatStatus(int nbr){
            String s = "| Stop   ";
            if(nbr==1){
                return "| Stand  ";
            }
            else if(nbr==2){
                return "| Ready  ";
            }
            return s;
        }
        public String formatTour(int nbr){
            String s = "";
            if((nbr%10)<10){
                s += "| " + nbr+"    ";
            }
            else {
                s += "| " + nbr+"   ";
            }
            return s;
        }
        public static int[][] CopyArray(int[][] source)
        {
            return source.Select(s => s.ToArray()).ToArray();
        }
    }
    
}