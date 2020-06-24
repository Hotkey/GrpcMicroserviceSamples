PASSWORD=grpc
if [ $# -ge 1 ]
  then
    PASSWORD=$1
fi

cat <<EOT >>dev.config
[ req ]
default_bits       = 2048
default_md         = sha256
default_keyfile    = dev.key
prompt             = no
encrypt_key        = no

distinguished_name = dn
req_extensions     = v3_req
x509_extensions    = x509_req
string_mask        = utf8only

[ dn ]
commonName             = localhost dev cert
emailAddress           = dev@opusgo.com
countryName            = TR
stateOrProvinceName    = ISTANBUL
localityName           = ISTANBUL
organizationName       = OPUSGO

[ x509_req ]
subjectKeyIdentifier   = hash
authorityKeyIdentifier = keyid,issuer
basicConstraints       = critical, CA:true
keyUsage               = critical, keyEncipherment
subjectAltName         = @alt_names
# extendedKeyUsage  = serverAuth, clientAuth
nsComment              = "OpenSSL Generated Certificate"

[ v3_req ]
subjectKeyIdentifier   = hash
basicConstraints       = critical, CA:true
subjectAltName         = @alt_names
# extendedKeyUsage  = serverAuth, clientAuth
nsComment              = "OpenSSL Generated Certificate"

[ alt_names ]
DNS.1                  = localhost
EOT

openssl req -config dev.config -new -out dev.csr.pem
openssl x509 -req -days 365 -extfile dev.config -extensions v3_req -in dev.csr.pem -signkey dev.key -out dev.crt
openssl pkcs12 -export -out dev.pfx -inkey dev.key -in dev.crt -password pass:$PASSWORD
rm dev.config dev.csr.pem
# cp dev.pfx ../GrpcTest