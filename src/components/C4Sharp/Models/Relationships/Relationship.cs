﻿namespace C4Sharp.Models.Relationships
{
    /// <summary>
    /// Relationship
    /// </summary>
    public record Relationship
    {
        public string From { get; }
        public string To { get; }
        public string Label { get; private set; }
        public string Protocol { get; private set; }
        public Position Position { get; private set; }
        public Direction Direction { get; }
        
        public Relationship this[string label]
        {
            get
            {
                Label = label;
                return this;
            }
        }
        
        public Relationship this[Position position]
        {
            get
            {
                Position = position;
                return this;
            }
        }        
        
        public Relationship this[string label, string protocol]
        {
            get
            {
                Label = label;
                Protocol = protocol;
                return this;
            }
        }        

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="from"></param>
        /// <param name="direction"></param>
        /// <param name="to"></param>
        /// <param name="label"></param>
        /// <param name="protocol"></param>
        /// <param name="position"></param>
        private Relationship(Structure @from, Direction direction, Structure to, string label,
            string protocol, Position position)
        {
            From = @from.Alias;
            To = to.Alias;
            Label = label;
            Direction = direction;
            Protocol = protocol;
            Position = position;
            
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <param name="label"></param>
        public Relationship(Structure @from, Structure to, string label)
            : this(from, Direction.Forward, to, label, string.Empty, Position.None)
        {
        }
        
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="from"></param>
        /// <param name="direction"></param>
        /// <param name="to"></param>
        /// <param name="label"></param>
        public Relationship(Structure @from, Direction direction, Structure to, string label)
            : this(from, direction, to, label, string.Empty, Position.None)
        {
        }
    }
}