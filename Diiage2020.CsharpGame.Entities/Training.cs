using System;
using System.Collections.Generic;
using System.Text;

namespace Diiage2020.CsharpGame.Entities
{
    public class Training
    {
        private static int _id = 0;
        public int Id { get; set; }

        public StaffMember StaffMember { get; set; }
        public TrainingSession TrainingSession { get; set; }
        public Turn StartTurn { get; set; }
        public Turn EndTurn { get; set; }

        public Training()
        {
            _id++;
            Id = _id;
        }
        public Training(StaffMember staffMember, TrainingSession trainingSession, Turn startTurn, Turn endTurn):this()
        {
            this.StaffMember = staffMember;
            this.TrainingSession = trainingSession;
            this.StartTurn = startTurn;
            this.EndTurn = endTurn;
        }
    }
}