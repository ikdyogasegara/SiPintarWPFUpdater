using System;
using System.Diagnostics.CodeAnalysis;

namespace SiPintar.Utilities
{
    [ExcludeFromCodeCoverage]
    public class ServiceResponse<TData, TError>
    {
        private TData _data;
        public TData Data
        {
            protected set => _data = value;
            get => _error != null ? throw new InvalidOperationException("Invalid access to Data property when Response is in Error") : _data;
        }

        private TError _error;
        public TError Error
        {
            protected set => _error = value;
            get => _data != null ? throw new InvalidOperationException("Invalid access to Error property when Response has Data") : _error;
        }

        public bool IsError => _error != null;
        public bool HasData => _data != null;

        public ServiceResponse(TData data) => Data = data;
        public ServiceResponse(TError error) => Error = error;
    }
}
