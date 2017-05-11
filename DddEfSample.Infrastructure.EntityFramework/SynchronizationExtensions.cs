using System;
using System.Collections.Generic;
using System.Linq;

namespace DddEfSample.Infrastructure.EntityFramework
{
    public static class SynchronizationExtensions
    {
        public static void SynchronizeWith<TRow, TModel, TKey>(this ICollection<TRow> rows, IEnumerable<TModel> models, Func<TRow, TKey> rowKeySelector, Func<TModel, TKey> modelKeySelector, Action<TRow, TModel> mapModelToRow)
            where TRow: new()
        {
            var rowIndex = rows.ToDictionary(rowKeySelector);
            var modelIndex = models.ToDictionary(modelKeySelector);

            var removedRows = rowIndex.Where(x => !modelIndex.ContainsKey(x.Key)).Select(x => x.Value);
            foreach (var row in removedRows)
            {
                rows.Remove(row);
            }

            var join = rowIndex.Join(modelIndex, x => x.Key, x => x.Key, (x, y) => new { Row = x.Value, Model = y.Value });
            foreach (var pair in join)
            {
                mapModelToRow(pair.Row, pair.Model);
            }

            var addedModels = modelIndex.Where(x => !rowIndex.ContainsKey(x.Key)).Select(x => x.Value);
            foreach (var model in addedModels)
            {
                var newRow = new TRow();
                mapModelToRow(newRow, model);
                rows.Add(newRow);
            }
        }
    }
}
