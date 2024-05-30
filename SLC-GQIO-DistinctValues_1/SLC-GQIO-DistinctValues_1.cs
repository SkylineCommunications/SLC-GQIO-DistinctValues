using Skyline.DataMiner.Analytics.GenericInterface;
using System.Collections.Generic;

[GQIMetaData(Name = "Get distinct values")]
public sealed class DistinctValuesOperator : IGQIRowOperator, IGQIInputArguments
{
    private readonly GQIArgument<GQIColumn> _columnArgument;

    private GQIColumn _column;
    private HashSet<object> _values;

    public DistinctValuesOperator()
    {
        _columnArgument = new GQIColumnDropdownArgument("Column") { IsRequired = true };
    }

    public GQIArgument[] GetInputArguments()
    {
        return new[] { _columnArgument };
    }

    public OnArgumentsProcessedOutputArgs OnArgumentsProcessed(OnArgumentsProcessedInputArgs args)
    {
        _column = args.GetArgumentValue(_columnArgument);

        return default;
    }

    public void HandleRow(GQIEditableRow row)
    {
        if (_values is null)
        {
            _values = new HashSet<object>();
        }

        var value = row.GetValue(_column.Name);
        if (!_values.Add(value))
        {
            row.Delete();
        }
    }
}