using System.Runtime.Serialization;
using ServiceSentry.Extensibility;

namespace ServiceSentry.Common.Web
{
    [DataContract, KnownType(typeof(Authority))]
    public class Authority : Equatable
    {
        private string _userInfo;
        private string _host;
        private int? _port;
        public static Authority Default => new Authority(null, null, null);

        internal Authority(string hostName) : this(null, hostName, null)
        {
        }

        internal Authority(string userInfo, string host):this(userInfo, host, null)
        {
        }

        internal Authority(string host, int port) : this(null, host, port)
        {
        }

        internal Authority(string userInfo, string host, int? port)
        {
            _host = host;
            _port = port;
            _userInfo = userInfo;
        }

        [DataMember]
        public string UserInfo
        {
            get => _userInfo;
            set
            {
                if (_userInfo == value) return;
                _userInfo = value;
                OnPropertyChanged(nameof(UserInfo));
            }
        }

        [DataMember]
        public string Host
        {
            get => _host;
            set
            {
                if (_host == value) return;
                _host = value;
                OnPropertyChanged(nameof(Host));
            }
        }

        [DataMember]
        public int? Port
        {
            get => _port;
            set
            {
                if (_port == value) return;
                _port = value;
                OnPropertyChanged(nameof(Port));
            }
        }
        
        /// <summary>
        ///     Converts the value of this <see cref="Authority" /> to its equivalent string representation.
        /// </summary>
        public override string ToString()
        {
            var userInfo = (string.IsNullOrWhiteSpace(UserInfo)) ? "" : $"{UserInfo}@";
            var port = (Port == null) ? "" : $":{Port}";
            return $"{userInfo}{Host}{port}";
        }

        /// <summary>
        ///     Determines whether the specifies <see cref="object" /> is equal to the current <see cref="Authority" />.
        /// </summary>
        /// <param name="obj">
        ///     The <see cref="object" /> to compare with the current <see cref="Authority" />.
        /// </param>
        public override bool Equals(object obj)
        {
            var item = obj as Authority;
            if (item == null)
            {
                return false;
            }

            var same = (UserInfo == item.UserInfo) && (Host == item.Host) && (Port == item.Port);
            return same;
        }

        /// <summary>
        ///     Returns the hashcode for this <see cref="Authority" />.
        /// </summary>
        public override int GetHashCode()
        {
            unchecked // Overflow is fine, just wrap.
            {
                // pick two prime numbers
                const int seed = 3;
                var hash = 29;

                // be sure to check for nullity, etc.
                hash *= seed + (!string.IsNullOrEmpty(UserInfo) ? UserInfo.GetHashCode() : 0);
                hash *= seed + (!string.IsNullOrEmpty(Host) ? Host.GetHashCode() : 0);
                hash *= seed + (Port != null ? Port.GetHashCode() : 0);

                return hash;
            }
        }

        [OnDeserialized]
        private void OnDeserialized(StreamingContext context)
        {
        }
    }
}