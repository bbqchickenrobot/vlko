﻿using Castle.ActiveRecord;

namespace vlko.model.Tests.Repository.NRepository.Implementation
{
    public enum TypeEnum
    {
        SomeFirstType,
        SomeOtherType,
        Ignore
    }
    [ActiveRecord]
    public class NTestObject
    {
        [PrimaryKey(PrimaryKeyType.Assigned)]
        public int ID { get; set; }

        [Property]
        public string Text { get; set; }

        [Property]
        public TypeEnum Type { get; set; }

        public override bool Equals(object obj)
        {
            if ((obj != null) && (obj is NTestObject))
            {
                var compare = obj as NTestObject;
                if (compare.ID.Equals(ID)
                    && (compare.Text.Equals(Text))
                    && (compare.Type.Equals(Type)))
                {
                    return true;
                }
            }
            return false;
        }
    }
}