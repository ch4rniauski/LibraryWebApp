import "./Pagination.css";
import * as React from 'react';
import Pagination from '@mui/material/Pagination';
import Stack from '@mui/material/Stack';

export default function PaginationControlled({setPage, page, pageLimit}) {
  const handleChange = (event, value) => {
    setPage(value);
  };

  return (
    <Stack spacing={2}>
      <Pagination className="Pagination" count={pageLimit} page={page} onChange={handleChange} />
    </Stack>
  );
}
