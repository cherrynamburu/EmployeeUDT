using Microsoft.SqlServer.Server;
using System;
using System.Data.SqlTypes;
using System.IO;
using System.Text;


[Serializable]
[SqlUserDefinedType(Format.UserDefined, IsByteOrdered = true, MaxByteSize = 5000, ValidationMethodName = "ValidateEmployee")]
public class Employee : INullable, IBinarySerialize
{

    const string nullMarker = "\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0";
    const int stringSize = 20;

    private bool is_Null;
    private string _name;
    private string _jobTitle;
    private decimal _salary;
    private int _previousExperienceInYears;

    
    public bool IsNull
    {
       get
        {
            return (is_Null);
        }
    }

    public static Employee Null
    {
        get
        {
            Employee employee = new Employee
            {
                is_Null = true
            };
            return employee;
        }

    }

    public String Name
    {
        get
        {
            return this._name;
        }
        set
        {
            String temp = _name;
            _name = value;
            if (!ValidateEmployee())
            {
                _name = temp;
                throw new ArgumentException("Invalid Employee attributes");
            }
        }
    }

    public String JobTitle
    {
        get
        {
            return this._jobTitle;
        }
        set
        {
            String temp = _jobTitle;
            _jobTitle = value;
            if (!ValidateEmployee())
            {
                _jobTitle = temp;
                throw new ArgumentException("Invalid Employee attributes");
            }
        }
    }

    public decimal Salary
    {
        get
        {
            return this._salary;
        }
        set
        {
            decimal temp = _salary;
            _salary = value;
            if (!ValidateEmployee())
            {
                _salary = temp;
                throw new ArgumentException("Invalid Employee attributes");
            }
        }
    }

    public int PreviousExperienceInYears
    {
        get
        {
            return this._previousExperienceInYears;
        }
        set
        {
            int temp = _previousExperienceInYears;
            _previousExperienceInYears = value;
            if (!ValidateEmployee())
            {
                _previousExperienceInYears = temp;
                throw new ArgumentException("Invalid Employee attributes");
            }
        }
    }

    public override string ToString()
    {
        if (this.IsNull)
            return "NULL";
        else
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(_name);
            builder.Append(",");
            builder.Append(_jobTitle);
            builder.Append(",");
            builder.Append(_salary);
            builder.Append(",");
            builder.Append(_previousExperienceInYears);
            return builder.ToString();
        }
    }

    [SqlMethod(OnNullCall = false)]
    public static Employee Parse(SqlString s)
    {
        if (s.IsNull)
            return Null;

        // Parsing input string to seperate employee elements.
        Employee employee = new Employee();
        string[] elements = s.Value.Split(",".ToCharArray());
        employee._name = elements[0].ToString();
        employee._jobTitle = elements[1].ToString();
        employee._salary = Convert.ToDecimal(elements[2]);
        employee._previousExperienceInYears = Convert.ToInt32(elements[3]);

        // To enforce validation for string conversions.
        if (!employee.ValidateEmployee())
            throw new ArgumentException("Invalid employee attribute values");
        return employee;
    }

    private bool ValidateEmployee()
    {
        if ((_salary >= 0) &&
            (_previousExperienceInYears >= 0) &&
            !String.IsNullOrEmpty(_name) &&
            !String.IsNullOrEmpty(_jobTitle))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void Write(BinaryWriter w)
    {
        if (w == null) throw new ArgumentNullException("w");
        if (this.IsNull)
        {
            w.Write(nullMarker);
            w.Write(nullMarker);
            w.Write((decimal)0);
            w.Write(0);
            return;
        }

        if (_name.Length >= stringSize)
        {
            throw new ApplicationException("Length is too long");
        }

        String paddedName = _name.PadRight(stringSize, '\0');
        for (int i=0; i<stringSize; i++)
        {
            w.Write(paddedName[i]);
        }


        if (_jobTitle.Length >= stringSize)
        {
            throw new ApplicationException("Length is too long");
        }

        string paddedJobTitle= _jobTitle.PadRight(stringSize, '\0');
        for (int i = 0; i < stringSize; i++)
        {
            w.Write(paddedJobTitle[i]);
        }
        w.Write(_salary);
        w.Write(_previousExperienceInYears);

    }

    public void Read(BinaryReader r)
    {
        char[] name = r.ReadChars(stringSize);
        int nameEnd = Array.IndexOf(name, '\0');

        if (nameEnd == 0)
        {
            _name = null;
            return;
        }
        _name = new String(name, 0, nameEnd);

        char[] jobTitle = r.ReadChars(stringSize);
        int jobTitleEnd = Array.IndexOf(jobTitle, '\0');

        if (jobTitleEnd == 0)
        {
            _jobTitle = null;
            return;
        }
        _jobTitle = new String(jobTitle, 0, jobTitleEnd);

        _salary = r.ReadDecimal();
        _previousExperienceInYears = r.ReadInt32();
    }


}

