import { inject } from '@angular/core';
import { HttpInterceptorFn } from '@angular/common/http';
import { finalize } from 'rxjs/operators';

import { Loading } from '../services/loading';

export const loadingInterceptor: HttpInterceptorFn = (req, next) => {

  const loading = inject(Loading);

  loading.show();

  return next(req).pipe(
    finalize(() => loading.hide())
  );

};