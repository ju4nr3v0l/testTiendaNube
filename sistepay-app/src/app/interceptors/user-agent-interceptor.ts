import { HttpInterceptorFn } from '@angular/common/http';

export const userAgentInterceptor: HttpInterceptorFn = (req, next) => {
  const clonedReq = req.clone({
    setHeaders: {
      'User-Agent': 'SistePayApp (sistecredito.com/contacto)'
    }
  });
  return next(clonedReq);
};
