namespace SqliteTools
{
    public enum DataUpdateMode
    {
        /// <summary>
        /// Delete data from table then insert
        /// </summary>
        DeleteThenInsert,
        /// <summary>
        /// Insert data
        /// </summary>
        Insert,
        /// <summary>
        /// Update rows based on the primary key from the table
        /// </summary>
        Update,
        /// <summary>
        /// Insert only new rows based on the primary key
        /// </summary>
        InsertOnlyNew
    }
}
