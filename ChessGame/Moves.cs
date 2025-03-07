using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessGame
{
    public class Moves
    {
        private List<Pos> Up;
        private List<Pos> Down;
        private List<Pos> Left;
        private List<Pos> Right;
        private List<Pos> UpRight;
        private List<Pos> UpLeft;
        private List<Pos> DownRight;
        private List<Pos> DownLeft;
        private List<Pos> Capture;
        private bool KingSideCastling;
        private bool QueenSideCastling;
        private List<Pos> Knight;

        public Moves()
        {
            Up = new List<Pos>();
            Down = new List<Pos>();
            Left = new List<Pos>();
            Right = new List<Pos>();
            UpRight = new List<Pos>();
            UpLeft = new List<Pos>();
            DownRight = new List<Pos>();
            DownLeft = new List<Pos>();
            Capture = new List<Pos>();
            Knight = new List<Pos>();
            KingSideCastling = false;
            QueenSideCastling = false;
        }
        public Moves(Moves m)
        {
            Up = new List<Pos>(m.GetUpMoves());
            Down = new List<Pos>(m.GetDownMoves());
            Left = new List<Pos>(m.GetLeftMoves());
            Right = new List<Pos>(m.GetRightMoves());
            UpRight = new List<Pos>(m.GetUpRightMoves());
            UpLeft = new List<Pos>(m.GetUpLeftMoves());
            DownRight = new List<Pos>(m.GetDownRightMoves());
            DownLeft = new List<Pos>(m.GetDownLeftMoves());
            Capture = new List<Pos>(m.GetCaptureMoves());
            Knight = new List<Pos>(m.GetKnightMoves());
            KingSideCastling = m.KingSideCastling;
            QueenSideCastling = m.QueenSideCastling;
        }
        public void Clear()
        {
            Up.Clear();
            Down.Clear();
            Left.Clear();
            Right.Clear();
            UpRight.Clear();
            UpLeft.Clear();
            DownRight.Clear();
            DownLeft.Clear();
            Capture.Clear();
            Knight.Clear();
            KingSideCastling = false;
            QueenSideCastling = false;
        }
        public void AddMoves(Moves m)
        {
            List<Pos> ls;
            ls = m.GetUpMoves();
            for (int i = 0; i < ls.Count; i++) Up.Add(ls[i]);
            ls = m.GetDownMoves();
            for (int i = 0; i < ls.Count; i++) Down.Add(ls[i]);
            ls = m.GetRightMoves();
            for (int i = 0; i < ls.Count; i++) Right.Add(ls[i]);
            ls = m.GetLeftMoves();
            for (int i = 0; i < ls.Count; i++) Left.Add(ls[i]);
            ls = m.GetUpRightMoves();
            for (int i = 0; i < ls.Count; i++) UpRight.Add(ls[i]);
            ls = m.GetUpLeftMoves();
            for (int i = 0; i < ls.Count; i++) UpLeft.Add(ls[i]);
            ls = m.GetDownRightMoves();
            for (int i = 0; i < ls.Count; i++) DownRight.Add(ls[i]);
            ls = m.GetDownLeftMoves();
            for (int i = 0; i < ls.Count; i++) DownLeft.Add(ls[i]);
            ls = m.GetCaptureMoves();
            for (int i = 0; i < ls.Count; i++) Capture.Add(ls[i]);
            ls = m.GetKnightMoves();
            for (int i = 0; i < ls.Count; i++) Knight.Add(ls[i]);
        }

        public bool IsPosInMoves(Pos p)
        {
           
                if (Up.Any(Pos => Pos.File == p.File && Pos.Rank == p.Rank))
                    return true;
                else if (Down.Any(Pos => Pos.File == p.File && Pos.Rank == p.Rank))
                    return true;
                else if (Left.Any(Pos => Pos.File == p.File && Pos.Rank == p.Rank))
                    return true;
                else if (Right.Any(Pos => Pos.File == p.File && Pos.Rank == p.Rank))
                    return true;
                else if (UpRight.Any(Pos => Pos.File == p.File && Pos.Rank == p.Rank))
                    return true;
                else if (UpLeft.Any(Pos => Pos.File == p.File && Pos.Rank == p.Rank))
                    return true;
                else if (DownRight.Any(Pos => Pos.File == p.File && Pos.Rank == p.Rank))
                    return true;
                else if (DownLeft.Any(Pos => Pos.File == p.File && Pos.Rank == p.Rank))
                    return true;
                else if (Knight.Any(Pos => Pos.File == p.File && Pos.Rank == p.Rank))
                    return true;
                else if (Capture.Any(Pos => Pos.File == p.File && Pos.Rank == p.Rank))
                    return true;
       
            return false;
        }
        public bool IsPosInCaptureMoves(Pos p)
        {

          if (Capture.Any(Pos => Pos.File == p.File && Pos.Rank == p.Rank))
                return true;

            return false;
        }
        public List<Pos> GetMoves()
        {
            List<Pos> ls = new List<Pos>();
            for (int i = 0; i < Up.Count; i++) ls.Add(Up[i]);
            for (int i = 0; i < Down.Count; i++) ls.Add(Down[i]);
            for (int i = 0; i < Left.Count; i++) ls.Add(Left[i]);
            for (int i = 0; i < Right.Count; i++) ls.Add(Right[i]);
            for (int i = 0; i < UpLeft.Count; i++) ls.Add(UpLeft[i]);
            for (int i = 0; i < DownLeft.Count; i++) ls.Add(DownLeft[i]);
            for (int i = 0; i < UpRight.Count; i++) ls.Add(UpRight[i]);
            for (int i = 0; i < DownRight.Count; i++) ls.Add(DownRight[i]);
            for (int i = 0; i < Knight.Count; i++) ls.Add(Knight[i]);
            for (int i = 0; i < Capture.Count; i++) ls.Add(Capture[i]);

            return ls;
        }
        public bool Remove (Pos p)
        {
            bool result;
            result=Up.Remove(p);
            if (result) return result;
            result=Down.Remove(p);
            if (result) return result;
            result = Left.Remove(p);
            if (result) return result;
            result = Right.Remove(p);
            if (result) return result;
            result = UpRight.Remove(p);
            if (result) return result;
            result = UpLeft.Remove(p);
            if (result) return result;
            result = DownRight.Remove(p);
            if (result) return result;
            result = DownLeft.Remove(p);
            if (result) return result;
            result = Capture.Remove(p);
            if (result) return result;
            result = Knight.Remove(p);
            if (result) return result;

            return false;
        }
        public int NumOfMoves()
        {
            int count = 0;
            count += Up.Count;
            count += Down.Count;
            count += Left.Count;
            count += Right.Count;
            count += UpRight.Count;
            count += DownLeft.Count;
            count += UpLeft.Count;
            count += DownRight.Count;
            count += Knight.Count;
            count += Capture.Count;
            
            if (KingSideCastling) count++;
            if (QueenSideCastling) count++;

            return count;
        }
        public List<Pos> GetUpMoves()
        {
           return Up;
        }
        public List<Pos> GetDownMoves()
        {
            return Down;
        }

        public List<Pos> GetRightMoves()
        {
            return Right;
        }
        public List<Pos> GetLeftMoves()
        {
            return Left;
        }
        public List<Pos> GetUpLeftMoves()
        {
            return UpLeft;
        }
        public List<Pos> GetUpRightMoves()
        {
            return UpRight;
        }
        public List<Pos> GetDownLeftMoves()
        {
            return DownLeft;
        }
        public List<Pos> GetDownRightMoves()
        {
            return DownRight;
        }
        public List<Pos> GetCaptureMoves()
        {
            return Capture;
        }
        public List<Pos> GetKnightMoves()
        {
            return Knight;
        }
        public void SetUpMoves(List<Pos> u)
        {
            Up = new List<Pos>(u);
        }
       
        public void SetDownMoves(List<Pos> d)
        {
            Down = new List<Pos>(d);
        }
       
        public void SetRightMoves(List<Pos> r)
        {
            Right = new List<Pos>(r);
        }
      

        public void SetLeftMoves(List<Pos> l)
        {
            Left = new List<Pos>(l);
        }
      
        public void SetDownRightMoves(List<Pos> dr)
        {
            DownRight = new List<Pos>(dr);
        }
        
        public void SetDownLeftMoves(List<Pos> dl)
        {
            DownLeft = new List<Pos>(dl);
        }
       
        public void SetUpRightMoves(List<Pos> ur)
        {
            UpRight = new List<Pos>(ur);
        }
        
        public void SetUpLeftMoves(List<Pos> ul)
        {
            UpLeft = new List<Pos>(ul);
        }
        
        public void SetKnightMoves(List<Pos> k)
        {
            Knight = new List<Pos>(k);
        }
        
        public void SetCaptureMoves(List<Pos> c)
        {
           Capture = new List<Pos>(c);
        }
        public void AddUpMove(Pos u, bool r)
        {
            if (!r)
                Up.Add(u);
            else
                Up.Insert(0, u);
        }
        public void AddCaptureMove(Pos c)
        {
         
                Capture.Add(c);
          
        }
        public void AddKnightMove(Pos k)
        {
           
                Knight.Add(k);
           
        }
        public void AddUpLeftMove(Pos ul,bool r)
        {
            if (!r)
                UpLeft.Add(ul);
            else
                UpLeft.Insert(0, ul);
        }
        public void AddDownLeftMove(Pos dl,bool r)
        {
            if (!r)
                DownLeft.Add(dl);
            else
                DownLeft.Insert(0, dl);
        }
        public void AddUpRightMove(Pos ur,bool r)
        {
            if (!r)
                UpRight.Add(ur);
            else
                UpRight.Insert(0, ur);
        }
        public void AddDownRightMove(Pos dr, bool r)
        {
            if (!r)
                DownRight.Add(dr);
            else
                DownRight.Insert(0, dr);
        }
        public void AddRightMove(Pos r, bool _r)
        {
            if (!_r)
                Right.Add(r);
            else
                Right.Insert(0, r);
        }
        public void AddLeftMove(Pos l, bool r)
        {
            if (!r)
                Left.Add(l);
            else
                Left.Insert(0, l);

        }
        public void AddDownMove(Pos d,bool r)
        {
            if (!r)
                Down.Add(d);
            else
                Down.Insert(0, d);
        }
        public bool KSCastle   // property
        {
            get { return KingSideCastling; }   // get method
            set { KingSideCastling = value; }   // set method
        }

        public bool QSCastle   // property
        {
            get { return QueenSideCastling; }   // get method
            set { QueenSideCastling = value; }   // set method
        }
        public void Diplay()
        {

         

            for(int i=0;i<Up.Count;i++)
                Console.Write(Up[i].File.ToString() + Up[i].Rank + ' ');

            for (int i = 0; i < Down.Count; i++)
                Console.Write(Down[i].File.ToString() + Down[i].Rank + ' ');

            for (int i = 0; i <Left.Count; i++)
                Console.Write(Left[i].File.ToString() + Left[i].Rank + ' ');

            for (int i = 0; i < Right.Count; i++)
                Console.Write(Right[i].File.ToString() + Right[i].Rank + ' ');

            for (int i = 0; i < UpLeft.Count; i++)
                Console.Write(UpLeft[i].File.ToString() + UpLeft[i].Rank + ' ');

            for (int i = 0; i < UpRight.Count; i++)
                Console.Write(UpRight[i].File.ToString() + UpRight[i].Rank + ' ');

            for (int i = 0; i < DownLeft.Count; i++)
                Console.Write(DownLeft[i].File.ToString() + DownLeft[i].Rank + ' ');

            for (int i = 0; i < DownRight.Count; i++)
                Console.Write(DownRight[i].File.ToString() + DownRight[i].Rank + ' ');

            for (int i = 0; i < Capture.Count; i++)
                Console.Write(Capture[i].File.ToString() + Capture[i].Rank + ' ');

            for (int i = 0; i < Knight.Count; i++)
                Console.Write(Knight[i].File.ToString() + Knight[i].Rank + ' ');
            
            Console.WriteLine("KingSideCastling:"+KingSideCastling.ToString() +' ');
            Console.WriteLine("QueenSideCastling:" + QueenSideCastling.ToString() + ' ');

        }
    }
}
